using System;
using UnityEngine;

namespace Enemy
{
    /// <summary>
    /// Contains data mostly about enemy party
    /// </summary>
    [Serializable]
    public class EncounterData
    {
        public EnemyType enemyType = EnemyType.Test;
        [Range(1, 100)] public int enemyCount = 1;
        [Range(1, 1000)] public int level = 1;
        [Range(1, 100000)] public int minHealth;
        [Range(1, 100000)] public int maxHealth;
        [Range(1, 100000)] public int minAttack;
        [Range(1, 100000)] public int maxAttack;
        [Range(1, 100000)] public int minSpeed;
        [Range(1, 100000)] public int maxSpeed;
    }
}