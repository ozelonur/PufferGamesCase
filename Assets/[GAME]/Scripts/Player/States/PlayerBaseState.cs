using _GAME_.Scripts.Core.StateMachine;
using _GAME_.Scripts.Extensions;
using UnityEngine;

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
        protected Camera mainCamera;
        protected LayerMask targetLayerMask;
        
        
        protected float speed;
        protected float visionRadius;

        protected Vector3 input;

        #endregion

        #region Constructor

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            rigidBody = this.stateMachine.playerRigidBody;
            speed = stateMachine.speed;
            playerAnimateController = this.stateMachine.playerAnimateController;
            moveTransform = this.stateMachine.playerMoveTransform;
            rotateTransform = this.stateMachine.playerRotateTransform;
            playerInputController = this.stateMachine.inputController;
            mainCamera = Camera.main;
            visionRadius = this.stateMachine.visionRadius;
            targetLayerMask = stateMachine.targetLayerMask;
            weaponTransform = stateMachine.weaponTransform;
        }

        #endregion

        #region Protected Methods

        protected void Move(float deltaTime)
        {
            if (!stateMachine.canLook)
            {
                rigidBody.MovePosition(moveTransform.position + rotateTransform.forward *
                    (input.magnitude * (speed * deltaTime)));
                playerAnimateController.Move(input.ToVector2XZ() != Vector2.zero
                    ? new Vector2(0, 1)
                    : Vector2.zero);
            }
            else
            {
                Vector3 direction = input.ToIso() * (input.magnitude * (speed * deltaTime));

                rigidBody.MovePosition(moveTransform.position + direction);

                Vector3 forwardDirection = rotateTransform.forward;
                Vector3 rightDirection = rotateTransform.right;

                Vector3 normalizedDirection = direction.normalized;

                float dotForward = Vector3.Dot(normalizedDirection.normalized, forwardDirection);
                float dotRight = Vector3.Dot(normalizedDirection.normalized, rightDirection);

                Vector2 animationDirection = new(dotRight, dotForward);
                playerAnimateController.Move(animationDirection);
            }
        }

        protected void Look(float deltaTime)
        {
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

                    rotateTransform.rotation = Quaternion.Slerp(rotateTransform.rotation, targetRotation,
                        stateMachine.mouseSensitivity * Time.deltaTime);
                }
            }

            else
            {
                if (input.ToVector2XZ() == Vector2.zero)
                {
                    return;
                }

                Vector3 relative = (moveTransform.position + input.ToIso()) -
                                   moveTransform.position;

                Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);

                rotateTransform.rotation =
                    Quaternion.RotateTowards(rotateTransform.rotation, rotation, stateMachine.turnSpeed * deltaTime);
            }
        }

        #endregion
    }
}