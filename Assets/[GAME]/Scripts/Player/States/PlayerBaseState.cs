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
        protected float speed;

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
            playerInputController = this.stateMachine.inputController;
        }

        #endregion

        #region Protected Methods

        protected void Move(float deltaTime)
        {
            if (!stateMachine.canLook)
            {
                rigidBody.MovePosition(moveTransform.position +
                                       input.ToIso() * (input.magnitude * (speed * deltaTime)));
                playerAnimateController.Move(input.ToVector2XZ());
            }
            else
            {
                rigidBody.MovePosition(moveTransform.position + moveTransform.forward *
                    (input.magnitude * (speed * deltaTime)));
                playerAnimateController.Move(input.ToVector2XZ() != Vector2.zero
                    ? new Vector2(0, 1)
                    : Vector2.zero);
            }
        }

        protected void Look(float deltaTime)
        {
            if (!stateMachine.canLook)
            {
                return;
            }

            if (input.ToVector2XZ() == Vector2.zero)
            {
                return;
            }

            Vector3 relative = (moveTransform.position + input.ToIso()) -
                               moveTransform.position;

            Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);

            moveTransform.rotation =
                Quaternion.RotateTowards(moveTransform.rotation, rotation, stateMachine.turnSpeed * deltaTime);
        }

        #endregion
    }
}