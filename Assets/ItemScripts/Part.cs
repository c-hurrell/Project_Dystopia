using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stat_Classes
{
    public class Part : Item
    {
        //[SerializeField] int _partId;
        [Header("Part Information")]
        [SerializeField] string partName, partType; // arm leg etc
        [Space]
        [Header("Part Level")]
        [SerializeField] private int partLvl;
        [Space]
        [Header("Part Stats")]
        [SerializeField] public StatType _statType, _statBonusType; // attack, defense etc
        [SerializeField] public int _statVal,_statBaseVal, _statBonusVal, _statBonusBaseVal;
        
        
        [SerializeField] public skill _partSkill;

        public enum StatType
        {
            Defence,
            Attack,
            Speed,
            Hp,
            Ep
        }

        private void Awake()
        {
            if (partLvl < 4)
            {
                
            }
        }
        

        // Skills associatted with parts?

        
        
    }
}
