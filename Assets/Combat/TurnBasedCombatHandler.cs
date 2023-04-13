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

            Debug.Log("Total player speed: " + totalPlayerSpeed);
            Debug.Log("Total enemy speed: " + totalEnemySpeed);

            if (totalEnemySpeed > totalPlayerSpeed)
            {
                Debug.Log("Enemy has more speed than players, enemy turn first");
                StartCoroutine(EnemyActCoroutine());
            }
            else
            {
                Debug.Log("Player has more speed than enemy, player turn first");
            }
        }

        /// <summary>
        /// Controls what the player does in the turn
        /// </summary>
        private void PlayerAct(PlayerActionType action, int targetEnemy)
        {
            PlayerAction playerAction = action switch
            {
                PlayerActionType.Attack => new AttackAction(targetEnemy),
                PlayerActionType.Defend => new DefendAction(),
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };

            playerAction.Execute(_enemyStatuses, _playerStatuses, _playerStatuses[_turnIndex]);
            _turnIndex++;

            if (_turnIndex >= _playerStatuses.Count)
            {
                StartCoroutine(WaitForDeathsAndEnemyTurn());
            }
        }

        private IEnumerator WaitForDeathsAndEnemyTurn()
        {
            var deaths = _enemyStatuses.Where(x => x.dying).ToList();
            Debug.Log("Waiting for " + deaths.Count + " enemies to die");
            yield return new WaitWhile(() =>
            {
                deaths.RemoveAll(x => !x.dying);
                return deaths.Count > 0;
            });


            _enemyStatuses.RemoveAll(x => x == null);
            _playerStatuses.RemoveAll(x => x == null);

            if (_enemyStatuses.Count == 0)
            {
                Debug.Log("Player won");
                CombatManager.EndBattle();
                yield break;
            }

            if (_playerStatuses.Count == 0)
            {
                Debug.Log("Player lost");
                CombatManager.EndBattle();
                yield break;
            }

            _turnIndex = 0;
            StartCoroutine(EnemyActCoroutine());
        }

        public void PlayerAttack()
        {
            if (_isEnemyTurn) return;
            PlayerAct(PlayerActionType.Attack, _targetEnemy);
        }

        public void PlayerDefend()
        {
            if (_isEnemyTurn) return;
            PlayerAct(PlayerActionType.Defend, _targetEnemy);
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

            foreach (var status in _enemyStatuses)
            {
                // theres only attack now
                var attack = new EnemyAttackAction();
                attack.Execute(_enemyStatuses, _playerStatuses, status);
                yield return new WaitForSeconds(EnemyTurnDelay);
            }
            
            Debug.Log("Enemy's turn is over");

            _isEnemyTurn = false;

            // stop player defending as turn is over
            foreach (var playerStatus in _playerStatuses)
            {
                playerStatus.StopDefend();
            }
        }
    }
}