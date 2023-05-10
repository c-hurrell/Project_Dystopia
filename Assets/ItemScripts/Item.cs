using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Details")]
    public int itemID;
    //public string itemType; - > Change to Enum?
    [SerializeField] public string itemDescription; // A description of the item
    [Header("Item Sprite")]
    [SerializeField] public Sprite sprite; // An image of the item

    public virtual void UseItem()
    {
        // Implement the use of the item here
    }
    private void Start()
    {
        Vector3 hideSelf = new Vector3(0, 0, 10);
        gameObject.transform.position = hideSelf;
        DontDestroyOnLoad(this);
    }

}
