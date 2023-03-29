using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;



    public class Enemy_Character : Character    //maybe should be a virtual class for specific enemy types to inherit from
    {
        [SerializeField] private Inventory inventory;
        public int enemyID; 
        //List<item> dropList;
        List<GameObject> partDrops;
        private GameObject droppedPart;
        
        // item selectdrop()
        // {
        //     int selector = Random.Range(0,dropList.Count);
        //     return dropList[selector];
        //
        // }*/
        private void Start()
        {
            partDrops.Add(head);
            partDrops.Add(chest);
            partDrops.Add(arms);
            partDrops.Add(legs);
        }
        void OnDeath()
        {
            int partIndex = Random.Range(0,partDrops.Count);
            droppedPart = partDrops[partIndex];
            inventory.AddPart(droppedPart);
        }
    }

