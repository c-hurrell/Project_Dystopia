using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stat_Classes
{
    public class Part : Item
    {
        //[SerializeField] int _partId;
        [Header("Part Information")]
        [SerializeField] private string _partName;
        [SerializeField] public PartType partType;
        [Space]
        [Header("Part Level")] 
        [SerializeField] private int _partLvl;

        [Space] [Header("Part Stats")] 
        [SerializeField] public StatType _statType; // _statBonusType; // attack, defense etc

        [SerializeField] public double _statVal, _statBaseVal; //, _statBonusVal, _statBonusBaseVal;
        
        
        [SerializeField] public skill _partSkill;

        // [SerializeField] _partSkill
        private void Awake()
        {
            CalculateStatValue();
        }
        public enum StatType
        {
            Defence,
            Attack,
            Speed,
            Hp,
            Ep
        }
        public enum PartType
        {
            Head,
            Arms,
            Chest,
            Legs
        }
        
        private void CalculateStatValue()
        {
            _statVal = _statBaseVal + _statBaseVal * (_partLvl * 0.1);
        }
    }
}
