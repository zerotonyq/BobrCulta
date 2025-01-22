namespace Utils.Initialize
{
    public interface IInitializableMono
    {
        public bool Initialized { get;  }
        void Initialize();
    }
}