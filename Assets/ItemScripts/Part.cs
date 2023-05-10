using System;
using System.Collections.Generic;
using UnityEngine;

namespace Stat_Classes
{
    public class Part : Item
    {
        //[SerializeField] int _partId;
        [Header("Part Information")]
        [SerializeField] public string _partName;
        [SerializeField] public PartType partType;
        [Space]
        [Header("Part Level")] 
        [SerializeField] public int _partLvl;

        [Space] [Header("Part Stats")] 
        [SerializeField] public StatType _statType; // _statBonusType; // attack, defense etc

        [SerializeField] public double _statVal, _statBaseVal; //, _statBonusVal, _statBonusBaseVal;
        
        
        [SerializeField] public skill _partSkill;

        // [SerializeField] _partSkill
        private void Awake()
        {
            CalculateStatValue();
        }
        private void Start()
        {
            Vector3 hideSelf = new Vector3(0, 0, 10);
            gameObject.transform.position = hideSelf;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject gm = GameObject.FindWithTag("GameManager");
            gameObject.transform.SetParent(gm.transform);
            DontDestroyOnLoad(this);
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
        
        public void CalculateStatValue()
        {
            _statVal = _statBaseVal + _statBaseVal * (_partLvl * 0.1);
        }
    }
}
