using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stat_Classes
{
    public class Part : Item
    {
        //[SerializeField] int _partId;
        [SerializeField] string _partName, _partType; // arm leg etc
        
        [SerializeField] public StatType _statType, _statBonusType; // attack, defense etc
        [SerializeField] public int _statVal,_statBaseVal, _statBonusVal, _statBonusBaseVal;

        [SerializeField] private int partLvl;
        
        // [SerializeField] _partSkill

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
