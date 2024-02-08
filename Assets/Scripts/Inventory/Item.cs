using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public string title;
    public string description;
    public Sprite icon;
    public int cooldown;

    public Item(int id, string title, string description, int cooldown) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.cooldown = cooldown;
    }


}
