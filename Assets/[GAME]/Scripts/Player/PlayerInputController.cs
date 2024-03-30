using UnityEngine;
using UnityEngine.InputSystem;

namespace _GAME_.Scripts.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        #region Public Variables

        public Vector2 moveVector;
        public Vector2 mouseDelta;

        #endregion

        #region Private Variables

        private PlayerInputActions _playerInputActions;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _playerInputActions = new();

            _playerInputActions.Player.Movement.performed += Move;
            _playerInputActions.Player.Look.performed += MouseLookPerformed;
            _playerInputActions.Player.Movement.canceled += Move;
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        #endregion

        #region Private Methods

        private void Move(InputAction.CallbackContext context)
        {
            moveVector = context.phase switch
            {
                InputActionPhase.Performed => context.ReadValue<Vector2>(),
                InputActionPhase.Canceled => Vector2.zero,
                _ => moveVector
            };
        }

        private void MouseLookPerformed(InputAction.CallbackContext context)
        {
            mouseDelta = context.ReadValue<Vector2>();
        }

        #endregion
    }
}