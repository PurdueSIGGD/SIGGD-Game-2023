using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitHotbarUI : MonoBehaviour
{
    public int selectedUnit = 0;
    public float selectedCost = 100;

    private bool blackout;
    private int currentUnits = 0;
    private const int maxUnits = 10;
    private List<UnitType> hotbar = new List<UnitType>(maxUnits);
    private GameObject hotbarUI;

    [SerializeField] private UnitLevelManager unitLevelManager;

    [SerializeField] bool isTest;

    // Start is called before the first frame update
    void Start()
    {
        // Find hotbarUI
        GameObject playerUIBars = FindObjectOfType<uiBarManager>().gameObject;
        hotbarUI = playerUIBars.transform.GetChild(0).GetChild(5).gameObject;
        //StartCoroutine(UITest());
        if (isTest) InsertUnits();
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
        } else
        {
            GameObject unit = unitLevelManager.unitFamilies[(int)unitNumber].members[0];
            selectedCost = unit.GetComponent<Unit>().manaCost;
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

        float mouseDelta = Input.mouseScrollDelta.y;
        int newSelectedUnit = ((((int)mouseDelta + selectedUnit) % currentUnits) + currentUnits) % currentUnits;

        GameObject currentUnitOverlay = hotbarUI.transform.GetChild(newSelectedUnit).GetChild(0).gameObject;
        //byte rOverlayColor = (byte)(FindObjectOfType<uiBarManager>().unitLightColor.r * 255);
        //byte gOverlayColor = (byte)(FindObjectOfType<uiBarManager>().unitLightColor.g * 255);
        //byte bOverlayColor = (byte)(FindObjectOfType<uiBarManager>().unitLightColor.b * 255);
        currentUnitOverlay.GetComponent<Image>().color = FindObjectOfType<uiBarManager>().unitLightColor;

        if (selectedUnit == newSelectedUnit)
            return;

        // Deselect the previous unit and select the current unit
        GameObject previousUnitOverlay = hotbarUI.transform.GetChild(selectedUnit).GetChild(0).gameObject;
        previousUnitOverlay.SetActive(false);

        UnitType unitNumber = hotbar[newSelectedUnit];
        GameObject unit = unitLevelManager.unitFamilies[(int)unitNumber].members[0];
        selectedCost = unit.GetComponent<Unit>().manaCost;

        currentUnitOverlay.SetActive(true);

        selectedUnit = newSelectedUnit;
    }

    // For testing purposes
    IEnumerator UITest()
    {
        yield return new WaitForSeconds(3);

        InsertUnitIntoHotbar(UnitType.HEALER);

        yield return new WaitForSeconds(3);

        InsertUnitIntoHotbar(UnitType.HEALER);

        yield return new WaitForSeconds(3);

        InsertUnitIntoHotbar(UnitType.HEALER);
    }

    void InsertUnits()
    {
        UnitFamily[] units = unitLevelManager.unitFamilies;
        foreach (UnitFamily fam in units)
        {
            InsertUnitIntoHotbar(fam.family);
        }
    }
}
