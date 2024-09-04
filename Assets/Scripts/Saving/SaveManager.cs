using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;

/*[CustomEditor(typeof(SaveManager))]
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
            saveManager.StartCoroutine(saveManager.LoadGameString(stringIn));
        }
        
        GUILayout.Space(16);
        
        if (GUILayout.Button("Clear Save Data"))
        {
            SaveManager.ClearSave();
        }
    }
}*/

public class SaveManager : MonoBehaviour
{
    public SaveData saveData;

    private const string SaveGameKey = "SaveDate";

    public class SaveData
    {
        // Put all values that need to be saved here
        public int playerLevel;
        public int tutorialProgress;
        public Vector3 spawnPoint;
        public ObjectiveData[] objectives;
    }
    
    [Serializable]
    public struct ObjectiveData
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
        Debug.Log("GameObjectKey: " + gameObjectKey.name + "  |  objectiveType: " + objectiveType);
        if (saveData.objectives == null)
        {
            ObjectiveData firstObjective = new ObjectiveData(gameObjectKey, objectiveType);
            var newList = new List<ObjectiveData>();
            newList.Add(firstObjective);
            saveData.objectives = newList.ToArray();
            //ObjectiveData[] objectiveList = { firstObjective };
            //saveData.objectives = objectiveList;
            Debug.Log("Calling Save");
            SaveGame();
            return;
        }

        Debug.Log("Why are we here?");

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

        // Get tutorial progress
        saveData.tutorialProgress = FindObjectOfType<TutorialDirector>().tutorialProgress;

        Debug.Log("About to JSONify");
        var saveDataString = JsonUtility.ToJson(saveData);
        Debug.Log($"Save string: {saveDataString}");
        PlayerPrefs.SetString(SaveGameKey, saveDataString);
        Debug.Log("Saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(SaveGameKey))
        {
            var saveDataString = GetSaveString();
            StartCoroutine(LoadGameString(saveDataString));
        }
        else
        {
            saveData = new SaveData();
        }
    }

    // Load everything as it was during the save
    public IEnumerator LoadGameString(string saveDataString)
    {
        saveData = JsonUtility.FromJson<SaveData>(saveDataString);
        
        // Move the player to the spawn point if it is set
        if (saveData.spawnPoint != Vector3.zero)
        {
            var spawnPoint = saveData.spawnPoint;
            spawnPoint.y = 1;
            FindObjectOfType<Movement>().gameObject.transform.position = spawnPoint;
        }

        //Update the tutorial's progress status
        TutorialDirector tutorialDirector = FindObjectOfType<TutorialDirector>();
        tutorialDirector.tutorialProgress = saveData.tutorialProgress;


        //Restore the player's level
        /*FindObjectOfType<LightResource>().addLight(100f);
        for (var i = 1; i < saveData.playerLevel; i++) {
            FindObjectOfType<PlayerLevel>().levelUp();
        }*/
        yield return new WaitForSeconds(0.05f);

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
                    if (g.GetComponent<ChargePylon>() != null)
                    {
                        g.GetComponent<ChargePylon>().markPylonDone();
                    }
                    if (g.GetComponent<FinalPylon>() != null)
                    {
                        g.GetComponent<FinalPylon>().markPylonDone();
                    }
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
