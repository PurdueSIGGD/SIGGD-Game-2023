using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitHotbarUI : MonoBehaviour
{
    // HEALTH SLIDER GOES FROM 0 TO 0.25


    [HideInInspector] public int selectedUnit = 0;
    [HideInInspector] public UnitType selectedType;
    [HideInInspector] public float selectedCost = 100;

    private bool blackout;
    private int currentUnits = 0;
    private const int maxUnits = 5;
    private List<UnitType> hotbar = new List<UnitType>(maxUnits);
    private Image unitImage;
    private GameObject hotbarUI;

    [SerializeField] private UnitLevelManager unitLevelManager;

    [SerializeField] bool isTest;

    // Start is called before the first frame update
    void Start()
    {
        // Find hotbarUI
        GameObject playerUIBars = FindObjectOfType<uiBarManager>().gameObject;
        hotbarUI = playerUIBars.transform.GetChild(0).GetChild(1).GetChild(2).gameObject;

        // Set unitImage as the GameObject image showing which unit is chosen
        unitImage = playerUIBars.transform.GetChild(0).GetChild(1).GetChild(3).gameObject.GetComponent<Image>();
        if (isTest) StartCoroutine(UITest());
    }

    // Update is called once per frame
    void Update()
    {
        blackout = FindObjectOfType<uiBarManager>().blackout;
        SelectHotbarUnit();
    }

    // Inserts a new found unit into the hotbar
    public void InsertUnitIntoHotbar(UnitType unitNumber)
    {
        // Add the new unit to the list
        hotbar.Add(unitNumber);
        selectedType = hotbar[selectedUnit];

        // Activate one of the hotbars and insert sprite into the hotbar
        GameObject currentUnitHotbar = hotbarUI.transform.GetChild(currentUnits).gameObject;
        GameObject currentUnitSlot = currentUnitHotbar.transform.GetChild(1).gameObject;
        GameObject unitSpirteObject = unitLevelManager.unitFamilies[(int)unitNumber].members[0];
        var childCount = unitSpirteObject.transform.childCount;
        Sprite unitSprite = unitSpirteObject.transform.GetChild(childCount - 1).GetChild(0).GetComponent<SpriteRenderer>().sprite;
        currentUnitSlot.GetComponent<Image>().sprite = unitSprite;
        currentUnitHotbar.SetActive(true);

        // If it's not the 1st unit, deactivate its overlay
        GameObject currentUnitOverlay = hotbarUI.transform.GetChild(currentUnits).GetChild(0).gameObject;
        currentUnitOverlay.GetComponent<Image>().color = FindObjectOfType<uiBarManager>().unitLightColor;
        if (currentUnits != 0)
        {
            currentUnitOverlay.SetActive(false);
        } else {
            GameObject unit = unitLevelManager.unitFamilies[(int)unitNumber].members[0];
            selectedCost = unit.GetComponent<Unit>().manaCost;

            // Activate and set unitImage as the first unit
            unitImage.gameObject.SetActive(true);
            unitImage.sprite = unitSprite;
        }

        // If blackout happens, deactivate its overlay
        if (blackout)
        {
            currentUnitOverlay.SetActive(false);
        }

        currentUnits++;
    }

    // Uses the scrollwheel to locate the currently selected unit on the hotbar
    private void SelectHotbarUnit()
    {
        // If blackout happens, deactivate its overlay
        if (blackout)
        {
            hotbarUI.transform.GetChild(selectedUnit).GetChild(0).gameObject.SetActive(false);
            return;
        }

        if (currentUnits == 0)
            return;

        float mouseDelta = Input.mouseScrollDelta.y * -1;
        int newSelectedUnit = ((((int)mouseDelta + selectedUnit) % currentUnits) + currentUnits) % currentUnits;

        GameObject currentUnitOverlay = hotbarUI.transform.GetChild(newSelectedUnit).GetChild(0).gameObject;
        currentUnitOverlay.GetComponent<Image>().color = FindObjectOfType<uiBarManager>().unitLightColor;

        if (selectedUnit == newSelectedUnit)
            return;

        // Deselect the previous unit and select the current unit
        GameObject previousUnitOverlay = hotbarUI.transform.GetChild(selectedUnit).GetChild(0).gameObject;
        previousUnitOverlay.SetActive(false);

        UnitType unitNumber = hotbar[newSelectedUnit];
        selectedType = hotbar[newSelectedUnit];
        GameObject unit = unitLevelManager.unitFamilies[(int)unitNumber].members[0];
        selectedCost = unit.GetComponent<Unit>().manaCost;

        currentUnitOverlay.SetActive(true);

        // Activate and set unitImage as the selected unit
        var childCount = unit.transform.childCount;
        unitImage.sprite = unit.transform.GetChild(childCount - 1).GetChild(0).GetComponent<SpriteRenderer>().sprite;

        selectedUnit = newSelectedUnit;
    }

    // For testing purposes
    IEnumerator UITest()
    {
        UnitFamily[] units = unitLevelManager.unitFamilies;
        foreach (UnitFamily fam in units)
        {
            InsertUnitIntoHotbar(fam.family);
            yield return new WaitForSeconds(3);
        }
    }
}
