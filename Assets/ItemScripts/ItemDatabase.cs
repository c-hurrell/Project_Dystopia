using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    // A list to store all of the items in the game
    public List<Item> items = new List<Item>();
    
    // The path to the JSON file containing the item data
    public string jsonFilePath = "items.json";

    void Start()
    {
        // Load the item data
        LoadItemData();
    }

    // Loads items from JSON file
    void LoadItemData()
    {
        // Read the JSON file as a string
        string json = File.ReadAllText(jsonFilePath);

        // Parse the JSON string into a list of items
        List<Item> loadedItems = JsonUtility.FromJson<List<Item>>(json);

        // Add the loaded items to the item database
        items.AddRange(loadedItems);
    }
    
    // Used to add an item to the database debating it's use?
    public void AddItem(Item item)
    {
        items.Add(item);
    }

    // Used to remove an item (also debating use)
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    // A method to find an item by ID
    public Item FindItemByID(int id)
    {
        foreach (Item item in items)
        {
            if (item.itemID == id)
            {
                return item;
            }
        }
        return null;
    }
}

