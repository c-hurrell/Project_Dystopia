using System.Collections;
using UnityEngine;
using Utils;
using World;

namespace Combat
{
    public class PlayerBattleStatus : MonoBehaviour, ICopyFrom<PartyMemberData>
    {
        public int health;
        public int maxHealth;
        public int attack;
        public int speed;
        public bool defending;
        public bool dying;

        public void CopyFrom(PartyMemberData other)
        {
            health = other.Health;
            maxHealth = other.MaxHealth;
            attack = other.Attack;
            speed = other.Speed;
        }

        public void TakeDamage(int damage)
        {
            if (defending)
            {
                damage /= 2;
                damage = Mathf.Max(damage, 1);
            }

            health -= damage;
            Debug.Log("Player took damage " + damage + " and health is now " + health);

            if (health <= 0)
            {
                Debug.Log("Player died");
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            dying = true;
            yield return new WaitForSeconds(2.0f);
            dying = false;
            Destroy(gameObject);
        }

        public void Defend()
        {
            defending = true;
            Debug.Log("Player defended");
        }

        public void StopDefend()
        {
            if (!defending) return;
            defending = false;
            Debug.Log("Player stopped defending");
        }
    }
}