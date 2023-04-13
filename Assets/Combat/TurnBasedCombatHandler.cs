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
            if (enemyPrefabs == null)
            {
                Debug.LogWarning("Enemy prefabs are null");
                enemyPrefabs = Array.Empty<GameObject>();
            }

            if (playerPrefabs == null)
            {
                Debug.LogWarning("Player prefabs are null");
                playerPrefabs = Array.Empty<GameObject>();
            }

            if (enemyPrefabs.Length != Enum.GetValues(typeof(EnemyType)).Length)
            {
                Debug.LogWarning("Enemy prefabs are null or not all enemy types are assigned");
            }

            if (playerPrefabs.Length < ProgressionStatus.PartyMembers.Count)
            {
                Debug.LogWarning("Player prefabs are null or not all party members are assigned");
            }

            foreach (var partyMember in ProgressionStatus.PartyMembers)
            {
                var memberTypeIndex = (int)partyMember.MemberType;
                GameObject player;
                if (memberTypeIndex >= playerPrefabs.Length)
                {
                    Debug.LogWarning($"Party member {partyMember.MemberType} is not assigned to a prefab");
                    player = new();
                }
                else
                {
                    player = Instantiate(playerPrefabs[(int)partyMember.MemberType]);
                }

                var status = player.AddComponent<PlayerBattleStatus>();
                status.CopyFrom(partyMember);

                _playerStatuses.Add(status);
            }

            _playerStatuses = _playerStatuses.OrderBy(x => x.speed).ToList();

            var currentEncounter = CombatManager.CurrentEncounter;

            for (var i = 0; i < currentEncounter.enemyCount; i++)
            {
                var enemyTypeIndex = (int)currentEncounter.enemyType;
                GameObject enemy;
                if (enemyTypeIndex >= enemyPrefabs.Length)
                {
                    Debug.LogWarning($"Enemy type {currentEncounter.enemyType} is not assigned to a prefab");
                    enemy = new();
                }
                else
                {
                    enemy = Instantiate(enemyPrefabs[(int)currentEncounter.enemyType]);
                }

                var status = enemy.AddComponent<EnemyBattleStatus>();
                status.health = Random.Range(currentEncounter.minHealth, currentEncounter.maxHealth + 1);
                status.attack = Random.Range(currentEncounter.minAttack, currentEncounter.maxAttack + 1);
                status.speed = Random.Range(currentEncounter.minSpeed, currentEncounter.maxSpeed + 1);

                _enemyStatuses.Add(status);
            }

            var totalPlayerSpeed = _playerStatuses.Sum(x => x.speed);
            var totalEnemySpeed = _enemyStatuses.Sum(x => x.speed);

            if (totalEnemySpeed > totalPlayerSpeed)
            {
                StartCoroutine(EnemyActCoroutine());
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
            if (_isEnemyTurn) return;
            PlayerAct(PlayerActionType.Attack, _targetEnemy);
        }

        private bool _isEnemyTurn;

        /// <summary>
        /// Coroutine that runs after player's turn to handle enemy's turn
        /// </summary>
        private IEnumerator EnemyActCoroutine()
        {
            _isEnemyTurn = true;
            Debug.Log("Enemy's turn");
            yield return new WaitForSeconds(EnemyTurnDelay);

            for (var i = 0; i < _enemyStatuses.Count; i++)
            {
                // theres only attack now
                var attack = new EnemyAttackAction();
                attack.Execute(_enemyStatuses, _playerStatuses, _enemyStatuses[i]);
                yield return new WaitForSeconds(EnemyTurnDelay);
            }

            _isEnemyTurn = false;
        }
    }
}