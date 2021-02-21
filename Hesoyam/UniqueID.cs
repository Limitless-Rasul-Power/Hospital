namespace Hesoyam
{
    public abstract class UniqueID
    {
        private static int id = 0;
        public int ID { get; } = ++id;
    }
}
