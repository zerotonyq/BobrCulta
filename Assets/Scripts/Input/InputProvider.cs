namespace Input
{
    public static class InputProvider
    {
        public static InputSystem_Actions InputSystemActions { get; }
        
        static InputProvider()
        {
            InputSystemActions = new();
            InputSystemActions.Enable();
        }
    }
}