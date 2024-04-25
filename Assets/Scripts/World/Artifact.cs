using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Interactable
{
    // TODO: Replace this with the enum for the turret family
    [SerializeField] private GameObject turret;
    
    public override void interact()
    {
        if (!isUsed)
        {
            // Mark objective as done
            GivePlayerArtifact();
            SaveArtifact();
        }
        
        base.interact();
    }

    // TODO: Have this function make the turret prefab available to the player
    private void GivePlayerArtifact()
    {
        
    }
    
    public void MarkArtifactDone()
    {
        isUsed = true;
        GivePlayerArtifact();
    }
    
    private void SaveArtifact()
    {
        var saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(transform.position + Vector3.right * 10f);
        saveManager.MarkObjective(gameObject, SaveManager.ObjectiveType.Artifact);
    }
}
