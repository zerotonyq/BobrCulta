namespace Signals.Cutscenes
{
    public struct CutsceneEndSignal
    {
        public readonly CutsceneType Type;
        
        public CutsceneEndSignal(CutsceneType type) => Type = type;

        public enum CutsceneType
        {
            None, 
            Start,
            End
        }
    }
}