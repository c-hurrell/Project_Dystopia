using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;
using World;
using Random = UnityEngine.Random;

namespace Combat
{
    public class TurnBasedCombatHandler : MonoBehaviour
    {
        private int _turnIndex;

        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private GameObject[] playerPrefabs;

        private readonly List<EnemyBattleStatus> _enemyStatuses = new();
        private List<PlayerBattleStatus> _playerStatuses = new();

        private const float EnemyTurnDelay = 1f;
        
        private int _targetEnemy;

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
                status.CopyFrom(partyMember);

                _playerStatuses.Add(status);
            }

            _playerStatuses = _playerStatuses.OrderBy(x => x.speed).ToList();

            var currentEncounter = CombatManager.CurrentEncounter;

            for (var i = 0; i < currentEncounter.enemyCount; i++)
            {
                var enemy = Instantiate(enemyPrefabs[(int)currentEncounter.enemyType]);
                var status = enemy.AddComponent<EnemyBattleStatus>();
                status.health = Random.Range(currentEncounter.minHealth, currentEncounter.maxHealth + 1);

                _enemyStatuses.Add(status);
            }
        }

        /// <summary>
        /// Controls what the player does in the turn
        /// </summary>
        private void PlayerAct(PlayerActionType action, int targetEnemy)
        {
            var playerAction = action switch
            {
                PlayerActionType.Attack => new AttackAction(targetEnemy),
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };

            playerAction.Execute(_enemyStatuses, _playerStatuses, _playerStatuses[_turnIndex]);
            _turnIndex++;

            if (_turnIndex >= _playerStatuses.Count)
            {
                _turnIndex = 0;
                StartCoroutine(EnemyActCoroutine());
            }
        }

        public void PlayerAttack()
        {
            PlayerAct(PlayerActionType.Attack, _targetEnemy);
        }

        /// <summary>
        /// Coroutine that runs after player's turn to handle enemy's turn
        /// </summary>
        private IEnumerator EnemyActCoroutine()
        {
            yield return new WaitForSeconds(EnemyTurnDelay);

            for (var i = 0; i < _enemyStatuses.Count; i++)
            {
                // theres only attack now
                var attack = new EnemyAttackAction();
                attack.Execute(_enemyStatuses, _playerStatuses, _enemyStatuses[i]);
                yield return new WaitForSeconds(EnemyTurnDelay);
            }
        }
    }
}