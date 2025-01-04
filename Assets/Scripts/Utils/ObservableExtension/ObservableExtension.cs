using R3;
using UnityEngine.InputSystem;

namespace Utils.ObservableExtension
{
    public static class ObservableExtension
    {
        public static Observable<InputAction.CallbackContext> ToObservablePerformed(this InputAction inputAction)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                a => inputAction.performed += a,
                a => inputAction.performed -= a);
        }
        
        public static Observable<InputAction.CallbackContext> ToObservableCanceled(this InputAction inputAction)
        {
            return Observable.FromEvent<InputAction.CallbackContext>(
                a => inputAction.canceled += a,
                a => inputAction.canceled -= a);
        }
    }
}