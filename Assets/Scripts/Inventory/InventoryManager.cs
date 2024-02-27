using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    // Start out with one slot and be able to add more
    int numOfItems = 1;

    // Each item will have an ID that doesn't correspond with
    // its slot in the bar

    List<int> barState = new List<int>();

    int[] cooldowns = {0,0,0,0};


    // Start is called before the first frame update
    void Start() {
        barState.Add(1);
    }

    // Update is called once per frame
    void Update()
    {
        print(barState);
    }


    /*
     * This function increases the amount of cooldowns in the bar.
     * Will have UI code added later.
     */
    
    void increaseNumberOfItems() {
        barState.Add(numOfItems);
        numOfItems++;

        // UI Stuff
    }

    /*
     * This function decreases the amount of cooldowns in the bar.
     * Will have UI code added later.
     */

    void decreaseNumberOfItems() {
        numOfItems--;
        barState.RemoveAt(numOfItems);
        
        // UI Stuff
    }
}
