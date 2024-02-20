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
using System.IO;

public class Item : MonoBehaviour {
    [SerializeField] Image border;
    [SerializeField] Image background;
    [SerializeField] Image itemImage;
    [SerializeField] int itemId;
    [SerializeField] TextMeshProUGUI durationText;

    [SerializeField] Sprite icon;

    // Item Numbers are 1 indexed but the array is 0 indexed
    private int[] cooldowns = {50, 500, 5, 10, 153};
    private Sprite[] sprites;
    private float currentCooldown;
    private bool onCooldown;
    void Awake() {
        currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
        durationText.text = currentCooldown.ToString();
        onCooldown = false;
        itemImage.sprite = icon;
        itemImage.enabled = true;
    }

    private int itemNum2ArrayIndex(int ind) {return ind - 1;}

    // Update is called once per frame
    void Update() {
        if (onCooldown) {
            currentCooldown -= Time.deltaTime;
            durationText.text = ((int) (currentCooldown + 0.999)).ToString();
            if (currentCooldown <= 0) {
                onCooldown = false;
                currentCooldown = 0;
                durationText.text = ((int) currentCooldown).ToString();
            }
        }

        if (itemId == 1) {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
                Debug.Log(itemId + ": Pressed either above the keyboard or on the numpad");
            
                if (!onCooldown) {
                    onCooldown = true;
                    currentCooldown = cooldowns[itemNum2ArrayIndex(itemId)];
                    durationText.text = cooldowns[itemNum2ArrayIndex(itemId)].ToString();
                    useCooldown1();
                } else {
                    useOnCooldown();
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
                    useCooldown2();
                } else {
                    useOnCooldown();
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
                    useCooldown3();
                } else {
                    useOnCooldown();
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
                    useCooldown4();
                } else {
                    useOnCooldown();
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
                    useCooldown5();
                } else {
                    useOnCooldown();
                }
            }
        }        
    }

    private void useOnCooldown() {
        Debug.Log("Item: " + itemId + " is on cooldown for " + currentCooldown + " more seconds");
    }

    private void useCooldown1() {
        Debug.Log("Item Running: " + itemId);
    }

    private void useCooldown2() {
        Debug.Log("Item Running: " + itemId);
    }

    private void useCooldown3() {
        Debug.Log("Item Running: " + itemId);
    }

    private void useCooldown4() {
        Debug.Log("Item Running: " + itemId);
    }

    private void useCooldown5() {
        Debug.Log("Item Running: " + itemId);
    }
}
