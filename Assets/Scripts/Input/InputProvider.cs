using R3;
using UnityEngine.InputSystem;

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

        public static Observable<T> GetStreamInput<T>(InputAction inputAction) where T : struct
        {
            return Observable.EveryValueChanged(inputAction, ia => ia.ReadValue<T>());
        }
    }
}