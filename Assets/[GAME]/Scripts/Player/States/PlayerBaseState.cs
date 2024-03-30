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
        protected float speed;

        protected Vector3 input;
        protected Vector2 mouseDelta;

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
        }

        #endregion

        #region Protected Methods

        protected void Move(float deltaTime)
        {
            if (!stateMachine.canLook)
            {
                // rigidBody.MovePosition(moveTransform.position +
                //                        input.ToIso() * (input.magnitude * (speed * deltaTime)));
                // playerAnimateController.Move(input.ToVector2XZ());

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
                if (mouseDelta == Vector2.zero)
                {
                    return;
                }

                float yaw = mouseDelta.x * stateMachine.mouseSensitivity * deltaTime;

                Quaternion newRotation = Quaternion.Euler(0f, yaw, 0f) * rotateTransform.rotation;

                rotateTransform.rotation = newRotation;
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