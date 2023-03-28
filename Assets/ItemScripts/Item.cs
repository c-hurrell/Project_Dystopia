using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Details")]
    public int itemID;
    public string itemType;
    public string itemName; // The name of the item
    [SerializeField] public string itemDescription; // A description of the item
    [Header("Item Sprite")]
    [SerializeField] private Sprite itemImage; // An image of the item

    public virtual void UseItem()
    {
        // Implement the use of the item here
    }

}
