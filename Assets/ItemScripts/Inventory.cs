using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;

public class Inventory : MonoBehaviour
{
    [SerializeField] 
    [SerializeField] private Player_Character playerChar;

    public List<Item> Items;
    public List<Part> Parts;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindWithTag("Player");
    }
    
    // Functions for Adding and Removing items
    void AddItem(Item item)
    {
        Items.Add(item);
    }

    void RemoveItem(Item item)
    {
        var itemLoc = Items.IndexOf(item);
        Items.RemoveAt(itemLoc);
    }

    // Functions for Adding and Removing parts
    void AddPart(Part part)
    {
        Parts.Add(part);
    }

    void RemovePart(Part part)
    {
        var partLoc = Parts.IndexOf(part);
        Parts.RemoveAt(partLoc);

    }
    // Equips a part to the player
    void EquipPart(Part part)
    {
        switch (part.partType)
        {
            case Part.PartType.Head:
                RemovePart(part);
                AddPart(playerChar.headPart);
                playerChar.ChangePart(part);
                break;
            case Part.PartType.Arms:
                RemovePart(part);
                AddPart(playerChar.armsPart);
                playerChar.ChangePart(part);
                break;
            case Part.PartType.PowerCore:
                RemovePart(part);
                AddPart(playerChar.powerCore);
                playerChar.ChangePart(part);
                break;
            case Part.PartType.Chest:
                RemovePart(part);
                AddPart(playerChar.chestPart);
                playerChar.ChangePart(part);
                
                break;
            case Part.PartType.Legs:
                RemovePart(part);
                AddPart(playerChar.legsPart);
                playerChar.ChangePart(part);
                break;
            default:
                Debug.Log(" > Error: Part should have a type!");
                break;
        }
    }
}
