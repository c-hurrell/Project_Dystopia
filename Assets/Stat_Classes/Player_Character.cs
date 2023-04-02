using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;
using UnityEditor.Build.Content;
using UnityEngine.UI;

namespace Stat_Classes
{
    public class Player_Character : Character
    {
        [Header("Gear UI Slots")] 
        [SerializeField] private GameObject headUI, chestUI, armsUI, legsUI;

        private void Start()
        {
            headPart = head.GetComponent<Part>();
            chestPart = chest.GetComponent<Part>();
            armsPart = arms.GetComponent<Part>();
            legsPart = legs.GetComponent<Part>();
            
            headUI.GetComponent<Button>().image.sprite = headPart.sprite;
            chestUI.GetComponent<Button>().image.sprite = chestPart.sprite;
            armsUI.GetComponent<Button>().image.sprite = armsPart.sprite;
            legsUI.GetComponent<Button>().image.sprite = legsPart.sprite;
        }

        void UnEquip(Part part)
        {
            // To do: Potentially may leave this out as the player should always have an item equipped
        }
        public void ChangePart(GameObject partItem)
        {
            //GameObject toInventory;
            var part = partItem.GetComponent<Part>();
            Debug.Log(part.name);
            switch (part.partType)
            {
                case Part.PartType.Head:
                    Debug.Log("Head");
                    head = partItem;
                    headPart = part;
                    //toInventory = head;
                    break;
                case Part.PartType.Arms:
                    arms = partItem;
                    armsPart = part;
                    break;
                case Part.PartType.Chest:
                    Debug.Log("Chest");
                    chest = partItem;
                    chestPart = part;
                    break;
                case Part.PartType.Legs:
                    legs = partItem;
                    legsPart = part;
                    break;
                default:
                    Debug.Log(" > Error: Part doesn't have a type");
                    break;
            }
            UpdateUI();
        }
        public void UpdateUI()
        {
            // Update the UI for the equipped head
            if (headPart != null)
            {
                headUI.GetComponent<Button>().image.sprite = headPart.sprite;
            }
            else
            {
                headUI.GetComponent<Button>().image.sprite = null;
            }

            // Update the UI for the equipped arms
            if (armsPart != null)
            {
                armsUI.GetComponent<Button>().image.sprite = armsPart.sprite;
            }
            else
            {
                armsUI.GetComponent<Button>().image.sprite = null;
            }

            // Update the UI for the equipped chest
            if (chestPart != null)
            {
                chestUI.GetComponent<Button>().image.sprite = chestPart.sprite;
            }
            else
            {
                chestUI.GetComponent<Button>().image.sprite = null;
            }

            // Update the UI for the equipped legs
            if (legsPart != null)
            {
                legsUI.GetComponent<Button>().image.sprite = legsPart.sprite;
            }
            else
            {
                legsUI.GetComponent<Button>().image.sprite = null;
            }
        }
    }
    
}
