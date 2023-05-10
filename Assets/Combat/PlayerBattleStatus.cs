using System;
using System.Collections;
using Stat_Classes;
using UnityEngine;
using Utils;

namespace Combat
{
    public class PlayerBattleStatus : MonoBehaviour, ICopyFrom<Player_Character>
    {
        public double health;
        public double maxHealth;
        public double attack;
        public double speed;
        public bool defending;
        public bool dying;

        public void CopyFrom(Player_Character other)
        {
            health = other.currentHp;
            maxHealth = other.hp;
            attack = other.attack;
            speed = other.speed;
        }

        public void TakeDamage(double damage)
        {
            if (defending)
            {
                damage /= 2;
                damage = Math.Max(damage, 1);
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