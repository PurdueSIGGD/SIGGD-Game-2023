using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Interactable
{
    // TODO: Replace this with the enum for the turret family
    [SerializeField] private UnitType unitNumber;
    
    public override void interact()
    {
        if (!isUsed)
        {
            // Mark objective as done
            isUsed = true;
            GivePlayerArtifact();
            SaveArtifact();
            //isUsed = true;
        }
        
        base.interact();
    }

    // TODO: Have this function make the turret prefab available to the player
    private void GivePlayerArtifact()
    {
        GameObject unitUI =  GameObject.FindGameObjectWithTag("UIUnit");
        unitUI.GetComponent<UnitHotbarUI>().InsertUnitIntoHotbar(unitNumber);
        //Destroy(this.gameObject);
    }
    
    public void MarkArtifactDone()
    {
        isUsed = true;
        GivePlayerArtifact();
    }
    
    private void SaveArtifact()
    {
        var saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(transform.position + Vector3.right * 5f);
        saveManager.MarkObjective(gameObject, SaveManager.ObjectiveType.Artifact);
    }
}
