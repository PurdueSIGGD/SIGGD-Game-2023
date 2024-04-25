using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btn_Unit : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Should be called from |Button| component in Unity editor
    /// </summary>
    /// <param name="unitPrefab"></param>
    public void Z_SwitchCurrentUnit(GameObject unitPrefab)
    {
        UnitController unitController = player.GetComponent<UnitController>();
        Debug.Log(unitController == null);
        unitController.OnSelect(unitPrefab);
        Debug.Log("TEST1");
    }

}
