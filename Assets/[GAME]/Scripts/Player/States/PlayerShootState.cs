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
        private float _rotationSpeed;
        public float _rotationThreshold;
        private float _shortestDistance = float.MaxValue;
        private float _frameCount;

        #endregion

        #region Constructor

        public PlayerShootState(PlayerStateMachine stateMachine, Transform target) : base(stateMachine)
        {
            _target = target;
            _rotationSpeed = DataManager.Instance.GetPlayerBaseAttackData().rotationSpeed;
            _rotationThreshold = DataManager.Instance.GetPlayerBaseAttackData().rotationThreshold;
        }

        #endregion

        #region Inherit Methods

        public override void OnEnter()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            input = playerInputController.moveVector.ToVector3XZ();
            CheckTargets();

            if (_target != null)
            {
                float distanceToTarget = Vector3.Distance(moveTransform.position, _target.position);
                bool isNearEdgeOfVision =
                    distanceToTarget > (visionRadius * 0.9f) && distanceToTarget <= visionRadius;

                float angleBetweenInputAndForward =
                    Quaternion.Angle(stateMachine.playerRotateTransform.rotation, targetQuaternion);


                if (isNearEdgeOfVision && angleBetweenInputAndForward >= 120)
                {
                    playerAnimateController.SetLayerWeight(1, 0);
                    playerAnimateController.Shoot(false);
                    stateMachine.SwitchState(new PlayerMoveState(stateMachine));
                    return;
                }

                look = false;
                stateMachine.canLook = true;
            }

            if (RotateToTheTargetAndGetReachStatus())
            {
                playerAnimateController.SetLayerWeight(1, 1);
                playerAnimateController.Shoot(true);
            }
            else
            {
                playerAnimateController.SetLayerWeight(1, 0);
                playerAnimateController.Shoot(false);
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

            if (_target != null)
            {
                if (!colliders.Contains(_target.GetComponent<Collider>()))
                {
                    _target = null;
                }
            }

            _shortestDistance = float.MaxValue;

            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(moveTransform.position, collider.transform.position);

                if (distance < _shortestDistance)
                {
                    _shortestDistance = distance;
                    Collider nearestCollider = collider;

                    if (_target != null)
                    {
                        if (_target.transform != nearestCollider.transform)
                        {
                            _target = nearestCollider.transform;
                        }
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

            Vector3 targetLocalPosition = weaponTransform.InverseTransformPoint(_target.position);

            if (targetLocalPosition.x < 0)
            {
                targetRotation *= Quaternion.Euler(0, -10f, 0);
            }
            else
            {
                targetRotation *= Quaternion.Euler(0, 10f, 0);
            }

            rotateTransform.rotation =
                Quaternion.Slerp(rotateTransform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            return Quaternion.Dot(rotateTransform.rotation, targetRotation) > _rotationThreshold;
        }

        #endregion
    }
}