using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InventoryManager : MonoBehaviour {
    // public static InventoryManager instance;
    // private Item[] inventory;
    // private int numOfSlots = 5; // Random Value (Will Change)
    
    
    // Start is called before the first frame update
    void Start() {
        // inventory = new Item[5];
        // Item[0] = new Item(0, "item 0", "this is the 0th item", 5);
        // Item[1] = new Item(1, "item 1", "this is the 1st item", 5);
        // Item[2] = new Item(2, "item 2", "this is the 2nd item", 5);
        // Item[3] = new Item(3, "item 3", "this is the 3rd item", 5);
        // Item[4] = new Item(4, "item 4", "this is the 4th item", 5);



    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
            Debug.Log("1 Pressed either above the keyboard or on the numpad");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
            Debug.Log("2 Pressed either above the keyboard or on the numpad");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
            Debug.Log("3 Pressed either above the keyboard or on the numpad");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
            Debug.Log("4 Pressed either above the keyboard or on the numpad");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) {
            Debug.Log("5 Pressed either above the keyboard or on the numpad");
        }






    }
}
