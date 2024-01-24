using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Unit : MonoBehaviour
{
    [SerializeField] GameObject player;
    TurretController unitController;


    void Start()
    {
    }

    public void Z_SwitchCurrentUnit(GameObject unitPrefab)
    {
        unitController = player.GetComponent<TurretController>();
        Debug.Log(unitController == null);
        unitController.OnSelect(unitPrefab);
        Debug.Log("TEST1");
    }

}
