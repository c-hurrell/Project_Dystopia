using Combat;
using Utils;

namespace World
{
    public class PartyMemberData : ICopyFrom<PlayerBattleStatus>
    {
        public MemberType MemberType { get; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }

        public PartyMemberData(MemberType memberType, int maxHealth = 15, int attack = 10, int speed = 10)
        {
            MemberType = memberType;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Attack = attack;
            Speed = speed;
        }

        public void CopyFrom(PlayerBattleStatus other)
        {
            Health = other.health;
            MaxHealth = other.maxHealth;
            Attack = other.attack;
            Speed = other.speed;
        }
    }

    public enum MemberType
    {
        MainChar
    }
}