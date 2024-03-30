using _GAME_.Scripts.Extensions;

namespace _GAME_.Scripts.Player.States
{
    public class PlayerMoveState : PlayerBaseState
    {
        #region Inherit Methods

        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }


        public override void OnEnter()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            input = playerInputController.moveVector.ToVector3XZ();
            mouseDelta = playerInputController.mouseDelta;
            Look(deltaTime);
        }

        public override void OnFixedUpdate(float fixedDeltaTime)
        {
            Move(fixedDeltaTime);
        }

        public override void OnExit()
        {
        }

        #endregion
    }
}