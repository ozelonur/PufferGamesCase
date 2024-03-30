using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.Managers;
using UnityEngine;

namespace _GAME_.Scripts.Player.States
{
    public class PlayerShootState : PlayerBaseState
    {
        #region Private Variables

        private Transform _target;
        private float _rotationSpeed = 5;
        public float _rotationThreshold = 0.01f;

        #endregion

        #region Constructor

        public PlayerShootState(PlayerStateMachine stateMachine, Transform target) : base(stateMachine)
        {
            _target = target;
        }

        #endregion

        #region Inherit Methods

        public override void OnEnter()
        {
            Debug.Log("Shoot State!");
        }

        public override void OnUpdate(float deltaTime)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_target.position - rotateTransform.position);

            rotateTransform.rotation =
                Quaternion.Slerp(rotateTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            if (Quaternion.Dot(rotateTransform.rotation, targetRotation) > _rotationThreshold)
            {
                playerAnimateController.SetLayerWeight(1, 1);
                playerAnimateController.Shoot(true);
            }
            else
            {
                playerAnimateController.SetLayerWeight(1, 0);
                playerAnimateController.Shoot(false);
            }

            input = playerInputController.moveVector.ToVector3XZ();
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }

            Move(fixedDeltaTime);
        }

        public override void OnExit()
        {
        }

        public override void OnDrawGizmos()
        {
        }

        #endregion
    }
}