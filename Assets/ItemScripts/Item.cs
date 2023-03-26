using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public int itemID;
    public string itemType;
    public string itemName; // The name of the item
    public string itemDescription; // A description of the item
    public Sprite itemImage; // An image of the item

    public virtual void UseItem()
    {
        // Implement the use of the item here
    }

}
