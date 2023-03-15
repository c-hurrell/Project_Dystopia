using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;



    public class Enemy_Character : Character    //maybe should be a virtual class for specific enemy types to inherit from
    {
        List<item> dropList;
        item selectdrop()
        {
            int selector = Random.Range(0,dropList.length);
            return dropList[selector];

        }
    }

