using System;
using Enemy;
using UnityEngine;
using World;
using Random = UnityEngine.Random;

namespace Combat
{
    public class TurnBasedCombatHandler : MonoBehaviour
    {
        private int _turnIndex;
        private bool _playersTurn = true;

        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private GameObject[] playerPrefabs;

        private void Start()
        {
            if (enemyPrefabs == null || enemyPrefabs.Length != Enum.GetValues(typeof(EnemyType)).Length)
            {
                Debug.LogError("Enemy prefabs are null or not all enemy types are assigned");
                return;
            }

            if (playerPrefabs == null || playerPrefabs.Length < ProgressionStatus.PartyMembers.Count)
            {
                Debug.LogError("Player prefabs are null or not all party members are assigned");
                return;
            }

            foreach (var partyMember in ProgressionStatus.PartyMembers)
            {
                var player = Instantiate(playerPrefabs[(int)partyMember.MemberType]);
                var status = player.AddComponent<PlayerBattleStatus>();
                status.health = partyMember.Health;
            }

            var currentEncounter = CombatManager.CurrentEncounter;

            for (var i = 0; i < currentEncounter.enemyCount; i++)
            {
                var enemy = Instantiate(enemyPrefabs[(int)currentEncounter.enemyType]);
                var status = enemy.AddComponent<EnemyBattleStatus>();
                status.health = Random.Range(currentEncounter.minHealth, currentEncounter.maxHealth + 1);
            }
        }

        /// <summary>
        /// Controls what the player does in the turn
        /// </summary>
        public void PlayerAct(PlayerAction action)
        {
        }
    }
}