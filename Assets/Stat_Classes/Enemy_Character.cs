using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;



    public class Enemy_Character : Character    //maybe should be a virtual class for specific enemy types to inherit from
    {
        public int enemyID; 
        //List<item> dropList;
        List<Part> partDrops = new List<Part>();
        private Part droppedPart;
        
        // item selectdrop()
        // {
        //     int selector = Random.Range(0,dropList.Count);
        //     return dropList[selector];
        //
        // }*/
        private void Start()
        {
            partDrops.Add(headPart);
            partDrops.Add(chestPart);
            partDrops.Add(armsPart);
            partDrops.Add(legsPart);
            //partDrops.Add(personaCore);
            partDrops.Add(powerCore);
        }
        void OnDeath()
        {
            int partIndex = Random.Range(0,partDrops.Count);
            droppedPart = partDrops[partIndex];
        }
    }

