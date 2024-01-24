using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Unit : MonoBehaviour
{
    TurretController unitController;


    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        unitController = player.GetComponent<TurretController>();
    }

    public void Z_SwitchCurrentUnit(GameObject unitPrefab)
    {
        unitController.OnSelect(unitPrefab);
    }

}
