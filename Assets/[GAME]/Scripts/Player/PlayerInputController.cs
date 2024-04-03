using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using OrangeBear.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _GAME_.Scripts.Player
{
    public class PlayerInputController : Bear
    {
        #region Public Variables

        public Vector2 moveVector;

        #endregion

        #region Private Variables

        private PlayerInputActions _playerInputActions;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _playerInputActions = new();

            _playerInputActions.Player.Movement.performed += Move;
            _playerInputActions.Player.AttackRange.performed += RangeIndicator;
            _playerInputActions.Player.Dash.performed += DashPerformed;
            _playerInputActions.Player.ThrowGrenade.performed += ThrowGrenadePerformed;
            _playerInputActions.Player.Shotgun.performed += ShotgunPerformed;
            _playerInputActions.Player.Stun.performed += StunPerformed;
            _playerInputActions.Player.Movement.canceled += Move;
            _playerInputActions.Player.AttackRange.canceled += RangeIndicator;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _playerInputActions.Enable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _playerInputActions.Disable();
        }

        #endregion

        #region Private Methods

        private void Move(InputAction.CallbackContext context)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }

            moveVector = context.phase switch
            {
                InputActionPhase.Performed => context.ReadValue<Vector2>(),
                InputActionPhase.Canceled => Vector2.zero,
                _ => moveVector
            };
        }

        private void RangeIndicator(InputAction.CallbackContext context)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }

            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    Roar(CustomEvents.VisionRangeVisibility, true);
                    break;
                case InputActionPhase.Canceled:
                    Roar(CustomEvents.VisionRangeVisibility, false);
                    break;
            }
        }

        private void DashPerformed(InputAction.CallbackContext context)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }
            Roar(CustomEvents.Dash);
        }

        private void ThrowGrenadePerformed(InputAction.CallbackContext context)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }
            Roar(CustomEvents.ThrowGrenade);
        }

        private void ShotgunPerformed(InputAction.CallbackContext context)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }
            
            Roar(CustomEvents.EnableShotgun);
        }

        private void StunPerformed(InputAction.CallbackContext context)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }
            
            Roar(CustomEvents.ThrowOilBomb);
        }

        #endregion
    }
}