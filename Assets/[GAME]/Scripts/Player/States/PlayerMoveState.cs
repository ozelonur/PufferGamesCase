using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.Managers;

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
            if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameFailed)
            {
                return;
            }

            input = playerInputController.moveVector.ToVector3XZ();
            mouseDelta = playerInputController.mouseDelta;
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

        #endregion
    }
}