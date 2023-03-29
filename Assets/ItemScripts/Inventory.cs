using System.Collections;
using System.Collections.Generic;
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
    
    // Functions for Adding and Removing items - > Have been removed as only parts exist in Inventory
    // void AddItem(Item item)
    // {
    //     Items.Add(item);
    // }
    //
    // void RemoveItem(Item item)
    // {
    //     var itemLoc = Items.IndexOf(item);
    //     Items.RemoveAt(itemLoc);
    // }

    // Functions for Adding and Removing parts
    public void AddPart(GameObject part)
    {
        TestParts.Add(part);
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
            itemSlot.enabled = true;
            itemSlot.image.sprite = TestParts[i].GetComponent<Part>().sprite;
        }
        for (var i = partNum; i < itemSlots.Count; i++)
        {
            Debug.Log("I'm disabling buttons" + i);
            itemSlots[i].SetActive(false);
        }
    }
    // Equips a part to the player
    public void EquipPart(int partSlot)
    {
        var part = TestParts[partSlot].GetComponent<Part>(); 
        switch (part.partType)
        {
            case Part.PartType.Head:
                // Removes Part at this location
                RemovePart(partSlot);
                // Takes in GameObject
                AddPart(playerChar.head);
                playerChar.ChangePart(TestParts[partSlot]);
                headSlot.image.sprite = part.sprite;
                break;
            case Part.PartType.Arms:
                RemovePart(partSlot);
                AddPart(playerChar.arms);
                playerChar.ChangePart(TestParts[partSlot]);
                armsSlot.image.sprite = part.sprite;
                break;
            case Part.PartType.Chest:
                RemovePart(partSlot);
                AddPart(playerChar.chest);
                playerChar.ChangePart(TestParts[partSlot]);
                chestSlot.image.sprite = part.sprite;
                break;
            case Part.PartType.Legs:
                RemovePart(partSlot);
                AddPart(playerChar.legs);
                playerChar.ChangePart(TestParts[partSlot]);
                legsSlot.image.sprite = part.sprite;
                break;
            default:
                Debug.Log(" > Error: Part should have a type!");
                break;
        }
        SortPartList();
        
    }
}
