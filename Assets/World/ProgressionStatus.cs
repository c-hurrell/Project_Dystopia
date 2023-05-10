namespace World
{
    public static class ProgressionStatus
    {
        public static Area CurrentArea { get; } = Area.Default;
    }

    public enum Area
    {
        Default,
        Level1,
        Level2,
        Level3,
        Level4
    }
}