namespace Signals.Level
{
    public class LevelPassedSignal
    {
        public LevelPassedType PassedType;

        public enum LevelPassedType
        {
            None,
            Loose,
            Next,
            Win
        }
    }
}