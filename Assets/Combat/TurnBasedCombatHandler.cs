using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using World;
using Random = UnityEngine.Random;

namespace Combat
{
    public class TurnBasedCombatHandler : MonoBehaviour
    {
        private int _turnIndex;

        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private GameObject[] playerPrefabs;

        [SerializeField] private GameObject enemyHudPrefab;

        [SerializeField] private GameObject combatHudContainer;

        [SerializeField] private GameObject enemyContainer;

        [SerializeField] private GameObject combatUI;
        [SerializeField] private GameObject damageIndicator;

        private TextMeshProUGUI _playerHpText;
        private readonly List<TextMeshProUGUI> _enemyHpTexts = new();
        private readonly List<GameObject> _enemyHuds = new();

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

            // For each enemy in encounter
            for (var i = 0; i < currentEncounter.enemyCount; i++)
            {
                var enemyTypeIndex = (int)currentEncounter.enemyType;
                GameObject enemy;
                // Checks if enemy prefabs are present
                if (enemyTypeIndex >= enemyPrefabs.Length) // in test case type = 1 lenght is 2? 
                {
                    Debug.LogWarning($"Enemy type {currentEncounter.enemyType} is not assigned to a prefab");
                    enemy = new();
                }
                else
                {
                    // If present add enemy
                    enemy = Instantiate(enemyPrefabs[(int)currentEncounter.enemyType]);
                    // Added by C-Hurrell
                    enemyContainer.GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;
                    enemyContainer.GetComponent<SpriteRenderer>().color = enemy.GetComponent<SpriteRenderer>().color;
                    //
                }

                var status = enemy.AddComponent<EnemyBattleStatus>();
                status.health = Random.Range(currentEncounter.minHealth, currentEncounter.maxHealth + 1);
                status.attack = Random.Range(currentEncounter.minAttack, currentEncounter.maxAttack + 1);
                status.speed = Random.Range(currentEncounter.minSpeed, currentEncounter.maxSpeed + 1);
                status.maxHealth = status.health;

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

            if (enemyHudPrefab == null)
            {
                Debug.LogError("Enemy HUD prefab is null");
            }

            if (combatHudContainer == null)
            {
                Debug.LogError("Enemy HUD container is null");
            }
            else
            {
                _playerHpText = combatHudContainer.transform.Find("PlayerHPEP").Find("Health")
                    .GetComponent<TextMeshProUGUI>();
            }

            for (var i = 0; i < currentEncounter.enemyCount; i++)
            {
                var enemyHudStatus = Instantiate(enemyHudPrefab, combatHudContainer.transform);
                var enemyTransform = enemyHudStatus.transform;
                var pos = enemyTransform.localPosition;

                // -18 each offset
                enemyTransform.localPosition = new(
                    pos.x,
                    pos.y - i * 18,
                    pos.z
                );

                var enemyIndex = enemyTransform.Find("EnemyIndex");
                enemyIndex.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();

                var button = enemyTransform.Find("Enemy1Button").GetComponent<Button>();
                var targetEnemy = i;
                button.onClick.AddListener(() =>
                {
                    Debug.Log("Player targeting enemy " + targetEnemy);
                    _targetEnemy = targetEnemy;
                });

                _enemyHpTexts.Add(enemyTransform.Find("EnemyHealth").GetComponent<TextMeshProUGUI>());
                _enemyHuds.Add(enemyHudStatus);
            }

            UpdateHud();
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

            if (playerAction is AttackAction)
            {
                PlayerAttackEffect(_playerStatuses[_turnIndex].attack);
            }

            UpdateHud();

            _turnIndex++;

            if (_turnIndex >= _playerStatuses.Count)
            {
                StartCoroutine(WaitForDeathsAndEnemyTurn());
            }
        }

        private void PlayerAttackEffect(int damage)
        {
            GameObject text = Instantiate(damageIndicator);
            text.transform.SetParent(combatUI.transform);
            text.transform.Find("DamageIndicator").GetComponent<TextMeshProUGUI>().text = damage.ToString();

            
        }

        private IEnumerator WaitForDeathsAndEnemyTurn()
        {
            yield return WaitForDeaths();
            _turnIndex = 0;
            StartCoroutine(EnemyActCoroutine());
        }

        public void PlayerAttack()
        {
            if (_isEnemyTurn) return;
            // Added by C-Hurrell
            // Plays the hit audio
            GetComponent<AudioSource>().Play();
            //
            PlayerAct(PlayerActionType.Attack, _targetEnemy);
        }

        public void PlayerDefend()
        {
            if (_isEnemyTurn) return;
            PlayerAct(PlayerActionType.Defend, _targetEnemy);
        }

        public void PlayerRun()
        {
            if (_isEnemyTurn) return;
            Debug.Log("Player ran away");
            CombatManager.EndBattle();
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
                UpdateHud();
                yield return new WaitForSeconds(EnemyTurnDelay);
            }

            yield return WaitForDeaths();

            Debug.Log("Enemy's turn is over");

            _isEnemyTurn = false;

            // stop player defending as turn is over
            foreach (var playerStatus in _playerStatuses)
            {
                playerStatus.StopDefend();
            }
        }

        private IEnumerator WaitForDeaths()
        {
            var enemyDeaths = _enemyStatuses.Where(x => x.dying).ToList();
            if (enemyDeaths.Count > 0)
            {
                Debug.Log("Waiting for " + enemyDeaths.Count + " enemies to die");
                yield return new WaitWhile(() =>
                {
                    enemyDeaths.RemoveAll(x => !x.dying);
                    return enemyDeaths.Count > 0;
                });
            }

            var playerDeaths = _playerStatuses.Where(x => x.dying).ToList();
            if (playerDeaths.Count > 0)
            {
                Debug.Log("Waiting for " + playerDeaths.Count + " players to die");
                yield return new WaitWhile(() =>
                {
                    playerDeaths.RemoveAll(x => !x.dying);
                    return playerDeaths.Count > 0;
                });
            }

            RemoveDeletedEnemies();

            if (_enemyStatuses.Count == 0)
            {
                Debug.Log("Player won");
                CombatManager.EndBattle();
                yield break;
            }

            if (_playerStatuses.Count == 0)
            {
                Debug.Log("Player lost");
                CombatManager.EndBattle(EndBattleStatus.GameOver);
            }
        }

        private void RemoveDeletedEnemies()
        {
            _enemyStatuses.RemoveAll(x => x == null);
            _playerStatuses.RemoveAll(x => x == null);

            for (var i = 0; i < _enemyHuds.Count; i++)
            {
                var enemyHud = _enemyHuds[i];
                if (i > _enemyStatuses.Count - 1)
                {
                    Destroy(enemyHud);
                }
            }

            var removeRangeIndex = _enemyStatuses.Count;
            _enemyHuds.RemoveRange(removeRangeIndex, _enemyHuds.Count - removeRangeIndex);
            _enemyHpTexts.RemoveRange(removeRangeIndex, _enemyHpTexts.Count - removeRangeIndex);

            UpdateHud();
        }

        private void UpdateHud()
        {
            if (_playerStatuses.Count == 0) return;

            var playerStatus = _playerStatuses.First();
            var playerHealth = Math.Max(playerStatus.health, 0);
            _playerHpText.text = $"{playerHealth}/{playerStatus.maxHealth}";

            for (var i = 0; i < _enemyHpTexts.Count; i++)
            {
                var enemyStatus = _enemyStatuses[i];
                var health = Math.Max(enemyStatus.health, 0);
                _enemyHpTexts[i].text = $"{health}/{enemyStatus.maxHealth}";
            }
        }
    }
}