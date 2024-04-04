using _GAME_.Scripts.Core.StateMachine;
using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Player.States
{
    public abstract class PlayerBaseState : State
    {
        #region Protected Variables

        protected PlayerStateMachine stateMachine;
        protected PlayerAnimateController playerAnimateController;
        protected PlayerInputController playerInputController;
        protected Rigidbody rigidBody;
        protected Transform moveTransform;
        protected Transform rotateTransform;
        protected Transform weaponTransform;
        protected UnityEngine.Camera mainCamera;
        protected LayerMask targetLayerMask;
        protected NavMeshAgent navMeshAgent;

        protected Quaternion targetQuaternion;


        protected float speed;
        protected float visionRadius;
        protected bool look;

        protected Vector3 input;

        #endregion

        #region Constructor

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            rigidBody = this.stateMachine.playerRigidBody;
            speed = DataManager.Instance.GetPlayerMovementData().speed;
            playerAnimateController = this.stateMachine.playerAnimateController;
            moveTransform = this.stateMachine.playerMoveTransform;
            rotateTransform = this.stateMachine.playerRotateTransform;
            playerInputController = this.stateMachine.inputController;
            mainCamera = UnityEngine.Camera.main;
            visionRadius = DataManager.Instance.GetPlayerBaseAttackData().radius;
            targetLayerMask = stateMachine.targetLayerMask;
            weaponTransform = stateMachine.weaponTransform;

            navMeshAgent = this.stateMachine.navMeshAgent;
            navMeshAgent.speed = speed;
        }

        #endregion

        #region Protected Methods

        protected void Move(float deltaTime)
        {
            if (!stateMachine.canLook)
            {
                Vector3 targetPosition = moveTransform.position + rotateTransform.forward * (input.magnitude * speed);
                navMeshAgent.SetDestination(targetPosition);

                playerAnimateController.Move(input.ToVector2XZ() != Vector2.zero ? new Vector2(0, 1) : Vector2.zero);
            }
            else
            {
                Vector3 direction = input.ToIso() * (input.magnitude * speed);

                if (direction != Vector3.zero)
                {
                    targetQuaternion = Quaternion.LookRotation(direction, Vector3.up);
                }

                Vector3 targetPosition = moveTransform.position + direction;
                navMeshAgent.SetDestination(targetPosition);

                Vector3 normalizedDirection = direction.normalized;
                float dotForward = Vector3.Dot(normalizedDirection, rotateTransform.forward);
                float dotRight = Vector3.Dot(normalizedDirection, rotateTransform.right);

                Vector2 animationDirection = new Vector2(dotRight, dotForward);
                playerAnimateController.Move(animationDirection);
            }
        }

        protected void Look(float deltaTime)
        {
            if (!look)
            {
                return;
            }

            if (stateMachine.canLook)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (Vector3.Distance(moveTransform.position, hit.transform.position) < 2f)
                    {
                        return;
                    }

                    Vector3 lookAtTarget = hit.point;
                    lookAtTarget.y = rotateTransform.position.y;
                    Quaternion targetRotation = Quaternion.LookRotation(lookAtTarget - rotateTransform.position);

                    targetQuaternion = targetRotation;

                    rotateTransform.rotation = Quaternion.Slerp(rotateTransform.rotation, targetRotation,
                        DataManager.Instance.GetPlayerMovementData().mouseSensitivity * Time.deltaTime);
                }
            }

            else
            {
                if (input.ToVector2XZ() == Vector2.zero)
                {
                    return;
                }

                Vector3 relative = (moveTransform.position + input.ToIso()) - moveTransform.position;
                Quaternion targetRotation = Quaternion.LookRotation(relative, Vector3.up);

                float angle = Quaternion.Angle(rotateTransform.rotation, targetRotation);

                float baseTurnSpeed = DataManager.Instance.GetPlayerMovementData().turnSpeed;

                float angleFactor = Mathf.Clamp(angle / 180f, 0.1f, 1f);
                float turnSpeed = baseTurnSpeed * (1 + angleFactor);

                targetQuaternion = targetRotation;

                rotateTransform.rotation =
                    Quaternion.RotateTowards(rotateTransform.rotation, targetRotation, turnSpeed * deltaTime);
            }
        }

        #endregion
    }
}