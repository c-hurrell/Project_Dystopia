using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;
using UnityEditor.Build.Content;

namespace Stat_Classes
{
    public class Player_Character : Character
    {
        [SerializeField] private Inventory inventory;

        void Start()
        {
            inventory = gameManager.GetComponent<Inventory>();
        }

        void UnEquip(Part part)
        {
            // To do: Potentially may leave this out as the player should always have an item equiped
        }
    }
    
}
