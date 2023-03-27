namespace World
{
    public class PartyMemberData
    {
        public MemberType MemberType { get; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public PartyMemberData(MemberType memberType, int maxHealth = 100)
        {
            MemberType = memberType;
            MaxHealth = maxHealth;
            Health = maxHealth;
        }
    }

    public enum MemberType
    {
        MainChar
    }
}