using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : Interactable
{
    [SerializeField] public UnitHotbarUI unitHotbarUI;
    [SerializeField] public MiniMap miniMap;
    [SerializeField] public LightResource playerLight;
    [SerializeField] public Light pointLight;

    // TODO: Replace this with the enum for the turret family
    [SerializeField] private UnitType unitNumber;
    
    public override void interact()
    {
        if (!isUsed)
        {
            // Mark objective as done
            //isUsed = true;
            playerLight.addLight(1000f);
            miniMap.changeArtifactIcon(gameObject);
            GivePlayerArtifact();
            SaveArtifact();
            if (pointLight != null) pointLight.enabled = false;
            //gameObject.GetComponent<Collider>().enabled = false;
            //interactPrompt.showPrompt("");
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
        if (pointLight != null) pointLight.enabled = false;
    }
    
    private void SaveArtifact()
    {
        var saveManager = FindObjectOfType<SaveManager>();
        saveManager.SetSpawnPoint(transform.position + Vector3.back * 8f);
        saveManager.MarkObjective(gameObject, SaveManager.ObjectiveType.Artifact);
    }
}
