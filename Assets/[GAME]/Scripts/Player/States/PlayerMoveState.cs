using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.Managers;
using UnityEngine;

namespace _GAME_.Scripts.Player.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        #region Private Variables

        private float _shortestDistance = float.MaxValue;
        private float _frameCount;

        #endregion

        #region Inherit Methods

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }


        public override void OnEnter()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }

            _frameCount++;

            if (_frameCount >= 30)
            {
                _frameCount = 0;
                Collider[] colliders = Physics.OverlapSphere(moveTransform.position, visionRadius, targetLayerMask);


                Collider nearestCollider = null;
                _shortestDistance = float.MaxValue;

                foreach (Collider collider in colliders)
                {
                    float distance = Vector3.Distance(moveTransform.position, collider.transform.position);

                    if (distance < _shortestDistance)
                    {
                        _shortestDistance = distance;
                        nearestCollider = collider;
                    }
                }

                if (nearestCollider != null)
                {
                    float distanceToTarget = Vector3.Distance(moveTransform.position, nearestCollider.transform.position);
                    bool isNearEdgeOfVision = distanceToTarget > (visionRadius * 0.9f) && distanceToTarget <= visionRadius;

                    float angleBetweenInputAndForward = Quaternion.Angle(stateMachine.playerRotateTransform.rotation,
                        targetQuaternion);

                    if (!isNearEdgeOfVision || angleBetweenInputAndForward < 120)
                    {
                        stateMachine.SwitchState(new PlayerShootState(stateMachine, nearestCollider.transform));
                    }
                }
            }
            
            look = true;
            stateMachine.canLook = false;


            input = playerInputController.moveVector.ToVector3XZ();
            Look(deltaTime);
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
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(moveTransform.position, visionRadius);
        }

        #endregion
    }
}