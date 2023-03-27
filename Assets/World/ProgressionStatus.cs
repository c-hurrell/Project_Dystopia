using System.Collections.Generic;

namespace World
{
    public static class ProgressionStatus
    {
        public static Area CurrentArea { get; } = Area.Default;
        public static List<PartyMemberData> PartyMembers { get; } = new() { new(MemberType.MainChar) };
    }

    public enum Area
    {
        Default
    }
}