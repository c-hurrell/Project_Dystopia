using Combat;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyBattle : MonoBehaviour
    {
        [SerializeField] private Collider2D trigger;
        [SerializeField] private EncounterData enemyInfo;
        
        // Addition by C-Hurrell
        [SerializeField] private GameObject self;

        private void Start()
        {
            if (trigger == null)
            {
                Debug.LogError("Enemy battle trigger is null, assign trigger in editor");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered battle trigger");
                CombatManager.StartCombat(enemyInfo);
                Destroy(self);
                //Destroy(this);
            }
        }
    }
}