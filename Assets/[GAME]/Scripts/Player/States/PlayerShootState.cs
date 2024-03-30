using System.Linq;
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
        public float _rotationThreshold = 0.1f;
        private float _shortestDistance = float.MaxValue;

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
            input = playerInputController.moveVector.ToVector3XZ();
            CheckTargets();

            if (RotateToTheTargetAndGetReachStatus())
            {
                playerAnimateController.IdlingShoot(input == Vector3.zero);
                playerAnimateController.SetLayerWeight(1, 1);
                playerAnimateController.Shoot(true);
            }
            else
            {
                playerAnimateController.SetLayerWeight(1, 0);
                playerAnimateController.Shoot(false);
                playerAnimateController.IdlingShoot(false);
            }

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

        #region Private Methods

        private void CheckTargets()
        {
            Collider[] colliders = Physics.OverlapSphere(moveTransform.position, visionRadius, targetLayerMask);

            if (!colliders.Contains(_target.GetComponent<Collider>()))
            {
                _target = null;
            }

            _shortestDistance = float.MaxValue;

            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(moveTransform.position, collider.transform.position);

                if (distance < _shortestDistance)
                {
                    _shortestDistance = distance;
                    Collider nearestCollider = collider;

                    if (_target.transform != nearestCollider.transform)
                    {
                        _target = nearestCollider.transform;
                    }
                }
            }
        }

        private bool RotateToTheTargetAndGetReachStatus()
        {
            if (_target == null)
            {
                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
                return false;
            }
            
            Quaternion targetRotation = Quaternion.LookRotation(_target.position - rotateTransform.position);

            rotateTransform.rotation =
                Quaternion.Slerp(rotateTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            return Quaternion.Dot(rotateTransform.rotation, targetRotation) > _rotationThreshold;
        }

        #endregion
    }
}