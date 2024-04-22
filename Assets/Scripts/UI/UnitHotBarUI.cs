using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitHotbarUI : MonoBehaviour
{
    public int selectedUnit = 0;
    public float selectedCost = 100;

    private int currentUnits = 0;
    private const int maxUnits = 10;
    private List<UnitType> hotbar = new List<UnitType>(maxUnits);
    private GameObject hotbarUI;

    [SerializeField] private UnitLevelManager unitLevelManager;

    // Start is called before the first frame update
    void Start()
    {
        // Find hotbarUI
        GameObject playerUIBars = FindObjectOfType<uiBarManager>().gameObject;
        hotbarUI = playerUIBars.transform.GetChild(0).GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out scrolling -> get selectedUnit and selectedCost
        //unit.GetComponent<Unit>().manaCost;
        if (currentUnits > 0) 
            selectedUnit = ((int) Input.mouseScrollDelta.y) % currentUnits;
    }

    // Inserts a new found unit into the hotbar
    public void InsertUnitIntoHotbar(UnitType unitNumber)
    {
        // Add the new unit to the list
        hotbar.Add(unitNumber);

        // Activate one of the hotbars and insert sprite into the hotbar
        GameObject currentUnitSlot = hotbarUI.transform.GetChild(currentUnits).GetChild(1).gameObject;

        GameObject unit = unitLevelManager.unitFamilies[(int) unitNumber].members[0];
        //unit.sprite


        currentUnitSlot.SetActive(true);

        currentUnits++;

    }

    private void SelectHotbarUnit()
    {
    }
}
