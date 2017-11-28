namespace Ividatalink.TipsAndTricks
{
    public sealed class Singleton
    {
        private Singleton() { }
        public static Singleton Instance => new Singleton();
    }
}

