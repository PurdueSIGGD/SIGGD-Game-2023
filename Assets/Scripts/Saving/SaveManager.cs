using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveManager))]
internal class SaveManagerEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
            
        var saveManager = (SaveManager)target;
        
        if (GUILayout.Button("Print Current Save String"))
        {
            var currentSaveString = saveManager.GetSaveString();
            if (string.IsNullOrEmpty(currentSaveString))
            {
                currentSaveString = "Save String is empty";
            }
            Debug.Log(currentSaveString);
        }
        
        GUILayout.Space(16);
        
        GUILayout.Label("Input a Custom Save String Here");
        var stringIn = GUILayout.TextArea("");
        if (GUILayout.Button("Load this Save String"))
        {
            saveManager.LoadGameString(stringIn);
        }
        
        GUILayout.Space(16);
        
        if (GUILayout.Button("Clear Save Data"))
        {
            SaveManager.ClearSave();
        }
    }
}

public class SaveManager : MonoBehaviour
{
    private SaveData saveData;

    private const string SaveGameKey = "SaveDate";

    private class SaveData
    {
        // Put all values that need to be saved here
        public int playerLevel;
        public Vector3 spawnPoint;
        public ObjectiveData[] objectives;
    }
    
    [Serializable]
    private struct ObjectiveData
    {
        public string gameObjectName;
        public ObjectiveType objectiveType;
        
        public ObjectiveData(GameObject go, ObjectiveType objType)
        {
            gameObjectName = go.name;
            objectiveType = objType;
        }
    }

    private void Start()
    {
        LoadGame();
    }

    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        saveData.spawnPoint = spawnPoint;
        SaveGame();
    }
    
    public void MarkObjective(GameObject gameObjectKey, ObjectiveType objectiveType)
    {
        // Check to make sure there is no duplicates
        foreach (var objective in saveData.objectives)
        {
            if (objective.gameObjectName == gameObjectKey.name)
            {
                return;
            }
        }
        
        var tempList = new List<ObjectiveData>(saveData.objectives);
        tempList.Add(new ObjectiveData(gameObjectKey, objectiveType));
        saveData.objectives = tempList.ToArray();
        SaveGame();
    }

    // These are used to know the type of objective being saved
    // If the objective has not been reached, it is not saved
    [Serializable]
    public enum ObjectiveType
    {
        GameObjectDisabled, // If this objective exists, disable the GameObject
        Pylon, // If this objective exists, mark the Pylon as completed
        Artifact, // If this objective exists, mark the Artifact as completed
    }

    public string GetSaveString()
    {
        var saveDataString = PlayerPrefs.GetString(SaveGameKey);
        return saveDataString;
    }
    
    public string SetSaveString()
    {
        var saveDataString = PlayerPrefs.GetString(SaveGameKey);
        return saveDataString;
    }

    // Save
    public void SaveGame()
    {
        // Get the player's level
        saveData.playerLevel = FindObjectOfType<PlayerLevel>().currentLevel;
        
        var saveDataString = JsonUtility.ToJson(saveData);
        Debug.Log($"Save string: {saveDataString}");
        PlayerPrefs.SetString(SaveGameKey, saveDataString);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(SaveGameKey))
        {
            var saveDataString = GetSaveString();
            LoadGameString(saveDataString);
        }
        else
        {
            saveData = new SaveData();
        }
    }

    // Load everything as it was during the save
    public void LoadGameString(string saveDataString)
    {
        saveData = JsonUtility.FromJson<SaveData>(saveDataString);
        
        // Move the player to the spawn point if it is set
        if (saveData.spawnPoint != Vector3.zero)
        {
            var spawnPoint = saveData.spawnPoint;
            spawnPoint.y = 1;
            FindObjectOfType<Movement>().gameObject.transform.position = spawnPoint;
        }

        for (var i = 0; i < saveData.playerLevel; i++) {
            FindObjectOfType<PlayerLevel>().levelUp();
        }

        // Deal with all of the completed objectives
        foreach (var objective in saveData.objectives)
        {
            var g = GameObject.Find(objective.gameObjectName);
            switch (objective.objectiveType)
            {
                case ObjectiveType.GameObjectDisabled:
                    g.SetActive(false);
                    break;
                case ObjectiveType.Pylon:
                    g.GetComponent<ChargePylon>().markPylonDone();
                    break;
                case ObjectiveType.Artifact:
                    g.GetComponent<Artifact>().MarkArtifactDone();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public static void ClearSave()
    {
        PlayerPrefs.DeleteKey(SaveGameKey);
    }

    // Respawn player and mark correct objectives
    public void DeathLoadGame()
    {
        
    }
}
