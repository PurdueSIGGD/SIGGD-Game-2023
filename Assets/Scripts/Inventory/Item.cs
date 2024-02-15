// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Item {
//     public string title;
//     public string description;
//     public Sprite icon;
//     public int cooldown;

//     public Item(int id, string title, string description, int cooldown) {
//         this.id = id;
//         this.title = title;
//         this.description = description;
//         this.cooldown = cooldown;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour {
    [SerializeField] Image border;
    [SerializeField] Image background;
    [SerializeField] Image itemImage;
    [SerializeField] int itemId;
    [SerializeField] TextMeshProUGUI durationText;

    // Item Numbers are 1 indexed but the array is 0 indexed
    private int[] cooldowns = {50, 500, 5, 10, 153};
    private Sprite[] sprites;
    private int currentCooldown;
    private bool onCooldown;
    void Awake() {
        currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
        durationText.text = currentCooldown.ToString();
        onCooldown = false;
    }

    private int itemNum2ArrayIndex(int ind) {return ind - 1;}

    // Update is called once per frame
    void Update() {
        if (onCooldown) {
            currentCooldown--; // TODO: Make cooldown in seconds and not frames or find a good frame cooldowns
            durationText.text = currentCooldown.ToString();
            if (currentCooldown == 0) {
                onCooldown = false;
            }
        }

        if (itemId == 1) {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
                Debug.Log(itemId + ": Pressed either above the keyboard or on the numpad");
            
                if (!onCooldown) {
                    onCooldown = true;
                    currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
                    durationText.text = cooldowns[itemNum2ArrayIndex(itemId)].ToString();
                    // TODO: Run Item 1
                    Debug.Log("Item Running: " + itemId);
                } else {
                    Debug.Log("Item Cooldown: " + itemId);
                }
            }
        }
        else if (itemId == 2) {
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
                Debug.Log(itemId + ": Pressed either above the keyboard or on the numpad");
            
                if (!onCooldown) {
                    onCooldown = true;
                    currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
                    durationText.text = cooldowns[itemNum2ArrayIndex(itemId)].ToString();
                    // TODO: Run Item 2
                    Debug.Log("Item Running: " + itemId);
                } else {
                    Debug.Log("Item Cooldown: " + itemId);
                }
            }
        }
        else if (itemId == 3) {
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
                Debug.Log(itemId + ": Pressed either above the keyboard or on the numpad");
            
                if (!onCooldown) {
                    onCooldown = true;
                    currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
                    durationText.text = cooldowns[itemNum2ArrayIndex(itemId)].ToString();
                    // TODO: Run Item 3
                    Debug.Log("Item Running: " + itemId);
                } else {
                    Debug.Log("Item Cooldown: " + itemId);
                }
            }
        }
        else if (itemId == 4) {
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
                Debug.Log(itemId + ": Pressed either above the keyboard or on the numpad");
            
                if (!onCooldown) {
                    onCooldown = true;
                    currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
                    durationText.text = cooldowns[itemNum2ArrayIndex(itemId)].ToString();
                    // TODO: Run Item 4
                    Debug.Log("Item Running: " + itemId);
                } else {
                    Debug.Log("Item Cooldown: " + itemId);
                }
            }
        }
        else if (itemId == 5) {
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) {
                Debug.Log(itemId + ": Pressed either above the keyboard or on the numpad");
            
                if (!onCooldown) {
                    onCooldown = true;
                    currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
                    durationText.text = cooldowns[itemNum2ArrayIndex(itemId)].ToString();
                    // TODO: Run Item 5
                    Debug.Log("Item Running: " + itemId);
                } else {
                    Debug.Log("Item Cooldown: " + itemId);
                }
            }
        }
        
        
        
        
    }

    void updateDurationText() {

    }
}
