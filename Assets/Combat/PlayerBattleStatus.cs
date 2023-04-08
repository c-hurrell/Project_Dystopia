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

        public void CopyFrom(PartyMemberData other)
        {
            health = other.Health;
            maxHealth = other.MaxHealth;
            attack = other.Attack;
            speed = other.Speed;
        }
    }
}