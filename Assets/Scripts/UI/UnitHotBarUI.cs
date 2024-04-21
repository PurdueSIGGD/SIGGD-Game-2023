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
    private List<GameObject> hotbar = new List<GameObject>(maxUnits);
    private GameObject hotbarUI;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerUIBars = FindObjectOfType<uiBarManager>().gameObject;
        hotbarUI = playerUIBars.transform.GetChild(0).GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out scrolling -> get selectedUnit and selectedCost
        //unit.GetComponent<Unit>().manaCost;
        selectedUnit = ((int) Input.mouseScrollDelta.y) % currentUnits;
    }

    // Inserts a new found unit into the hotbar
    public void InsertUnitIntoHotbar(GameObject unit)
    {
        hotbar.Add(unit);
        currentUnits++;

    }

    private void SelectHotbarUnit()
    {
    }
}
