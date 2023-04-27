namespace World
{
    public class PartyMemberData
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
    }

    public enum MemberType
    {
        MainChar
    }
}