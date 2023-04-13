using System.Collections;
using UnityEngine;

namespace Combat
{
    public class EnemyBattleStatus : MonoBehaviour
    {
        public int health;
        public int attack;
        public int speed;

        public void TakeDamage(int damage)
        {
            health -= damage;
            Debug.Log("Enemy took damage " + damage + " and health is now " + health);

            if (health <= 0)
            {
                Debug.Log("Enemy died");
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            yield return new WaitForSeconds(2.0f);
            Destroy(gameObject);
        }
    }
}