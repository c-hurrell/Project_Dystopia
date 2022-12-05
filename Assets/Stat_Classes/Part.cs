using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stat_Classes
{
    public class Part : MonoBehaviour
    {
        [SerializeField]
        int part_id;
        [SerializeField]
        string part_name, part_type; // arm leg etc
        
        [SerializeField]
        string stat_type, stat_bonus_type; // attack, defense etc
        [SerializeField]
        int stat_val, stat_bonus_val;

        // Skills associatted with parts?

        
        
    }
}
