namespace Utils
{
    /// <summary>
    /// Interface for copying data from another object.
    /// </summary>
    public interface ICopyFrom<in T>
    {
        void CopyFrom(T other);
    }
}