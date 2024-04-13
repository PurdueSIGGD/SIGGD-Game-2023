using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Unit : MonoBehaviour
{
    [SerializeField] GameObject player;

    void Start()
    {
    }

    public void Z_SwitchCurrentUnit(GameObject unitPrefab)
    {
        UnitController unitController = player.GetComponent<UnitController>();
        Debug.Log(unitController == null);
        unitController.OnSelect(unitPrefab);
        Debug.Log("TEST1");
    }

}
