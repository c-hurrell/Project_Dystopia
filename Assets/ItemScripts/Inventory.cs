using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using UnityEngine.UI;
using Stat_Classes;

public class Inventory : MonoBehaviour
{
    [Header("Put Player Object Here")]
    [SerializeField] private GameObject player;
    [Header("Updated during gameplay")] 
    [SerializeField] private Player_Character playerChar;

    [Space] [Header("InventoryUI")] 
    [SerializeField] private GameObject inventoryUI;

    [Space] [Header("Equipment Slots")] 
    [SerializeField] private Button headSlot;
    [SerializeField] private Button armsSlot;
    [SerializeField] private Button chestSlot;
    [SerializeField] private Button legsSlot;

    [Space] [Header("Test Parts")] 
    [SerializeField] private List<GameObject> TestParts;
    [SerializeField] private List<GameObject> itemSlots;
    public List<Item> Items;
    public List<Part> Parts;
    
    // Testing Purposes


    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindWithTag("Player");
        playerChar = player.GetComponent<Player_Character>();
        
        // Testing purposes
        
        SortPartList();
    }

    void Update()
    {
        if (CombatManager.IsInCombat) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryUI();
        }
    }

    private void ToggleInventoryUI()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            inventoryUI.SetActive(true);
        }
    }
    
    // Functions for Adding and Removing parts
    public void AddPart(GameObject part)
    {
        TestParts.Add(part);
        SortPartList();
    }

    void RemovePart(int partSlot)
    {
        TestParts.RemoveAt(partSlot);
    }

    void SortPartList()
    {
        Debug.Log("I'm sorting");
        var partNum = TestParts.Count;
        for (var i = 0; i < partNum; i++)
        {
            Debug.Log("I'm sorting parts" + i);
            var itemSlot = itemSlots[i].GetComponent<Button>();
            itemSlots[i].SetActive(true);
            
            itemSlot.image.sprite = TestParts[i].GetComponent<Part>().sprite;
        }
        for (var i = partNum; i < itemSlots.Count; i++)
        {
            Debug.Log("I'm disabling buttons" + i);
            itemSlots[i].SetActive(false);
        }
    }
    public void EquipPart(int partSlot)
    {
        // Get the Part component of the inventory item
        var inventoryPart = TestParts[partSlot].GetComponent<Part>();

        // Part going to inventory
        GameObject toInventory;
        
        // Part data
        Part playerPart;
        
        // Copy data
        string partName;
        Part.StatType statType;
        Sprite sprite;
        double statBaseVal;
        int partLvl;

        switch (inventoryPart.partType)
        {
            case Part.PartType.Head:
                toInventory = playerChar.head;
                playerPart = playerChar.headPart;
                //playerChar.ChangePart(TestParts[partSlot]);
                break;
            case Part.PartType.Arms:
                toInventory = playerChar.arms;
                playerPart = playerChar.armsPart;
                //playerChar.ChangePart(TestParts[partSlot]);
                break;
            case Part.PartType.Chest:
                toInventory = playerChar.chest;
                playerPart = playerChar.chestPart;
                //playerChar.ChangePart(TestParts[partSlot]);
                break;
            case Part.PartType.Legs:
                toInventory = playerChar.legs;
                playerPart = playerChar.legsPart;
                //playerChar.ChangePart(TestParts[partSlot]);
                break;
            default:
                Debug.Log("Error: Part should have a type!");
                return;
        }
        // Add part
        toInventory.AddComponent<Part>();
        
        // Save Values
        partName = playerPart._partName;
        statType = playerPart._statType;
        sprite = playerPart.sprite;
        statBaseVal = playerPart._statBaseVal;
        partLvl = playerPart._partLvl;
        
        // Apply Values
        toInventory.GetComponent<Part>()._partName = partName;
        toInventory.GetComponent<Part>()._statType = statType;
        toInventory.GetComponent<Part>().sprite = sprite;
        toInventory.GetComponent<Part>()._statBaseVal = statBaseVal;
        toInventory.GetComponent<Part>()._partLvl = partLvl;
        toInventory.GetComponent<Part>().CalculateStatValue();
        // To do: Add Skill when implemented

        playerChar.ChangePart(TestParts[partSlot]);
        
        // Remove the Part from the inventory and add it to the player
        RemovePart(partSlot);
        AddPart(toInventory);

        // Swap the sprites in the inventory and gear UI
        playerChar.UpdateUI();

        // Recalculate the player's stats
        playerChar.StatTotals();
        
        SortPartList();
    }

}
