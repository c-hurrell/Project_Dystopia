using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stat_Classes
{
    public class Part : MonoBehaviour
    {
        [SerializeField] int _partId;
        [SerializeField] string _partName, _partType; // arm leg etc
        
        [SerializeField] StatType _statType, _statBonusType; // attack, defense etc
        [SerializeField] int _statVal, _statBonusVal;

        public enum StatType
        {
            Defence,
            Attack,
            Speed,
            Hp,
            Ep
        }

        // Skills associatted with parts?

        
        
    }
}
