using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Interactable
{
    [SerializeField] public UnitHotbarUI unitHotbarUI;
    [SerializeField] public MiniMap miniMap;

    // TODO: Replace this with the enum for the turret family
    [SerializeField] private UnitType unitNumber;
    
    public override void interact()
    {
        if (!isUsed)
        {
            // Mark objective as done
            isUsed = true;
            miniMap.changeArtifactIcon(gameObject);
            GivePlayerArtifact();
            SaveArtifact();
            //isUsed = true;
        }
        
        base.interact();
    }

    // TODO: Have this function make the turret prefab available to the player
    private void GivePlayerArtifact()
    {
        //GameObject unitUI =  GameObject.FindGameObjectWithTag("UIUnit");
        unitHotbarUI.InsertUnitIntoHotbar(unitNumber);
        //unitUI.GetComponent<UnitHotbarUI>().InsertUnitIntoHotbar(unitNumber);
        //Destroy(this.gameObject);
    }
    
    public void MarkArtifactDone()
    {
        isUsed = true;
        miniMap.changeArtifactIcon(gameObject);
        GivePlayerArtifact();
    }
    
    private void SaveArtifact()
    {
        var saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(transform.position + Vector3.right * 5f);
        saveManager.MarkObjective(gameObject, SaveManager.ObjectiveType.Artifact);
    }
}
