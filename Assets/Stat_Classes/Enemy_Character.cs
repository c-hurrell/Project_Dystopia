using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stat_Classes;

namespace Stat_Classes
{
    public class Enemy_Character : Character
    {
        List<item> dropList;
        item selectdrop()
        {
            int selector = Random.Range(0,dropList.length);
            return dropList[selector];

        }
    }
}
