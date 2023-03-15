using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;



    public class Enemy_Character : Character    //maybe should be a virtual class for specific enemy types to inherit from
    {
    /*    List<item> dropList;
        item selectdrop()
        {
            int selector = Random.Range(0,dropList.Count);
            return dropList[selector];

        }*/
        void onDeath(){
          List<Part> partDrops = new List<Part>();
          partDrops.Add(headPart);
          partDrops.Add(chestPart);
          partDrops.Add(armsPart);
          partDrops.Add(legsPart);
          partDrops.Add(personaCore);
          partDrops.Add(powerCore);

          int partIndex = Random.Range(0,partDrops.Count);

          Part droppedPart = partDrops[partIndex];
         

        }
    }

