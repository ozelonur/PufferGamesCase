using _GAME_.Scripts.Extensions;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using UnityEngine;

namespace _GAME_.Scripts.Player.States
{
    public class PlayerShotgunState : PlayerBaseState
    {
        #region Constructor

        public PlayerShotgunState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        #endregion

        #region Inherit Methods

        public override void OnEnter()
        {
            look = true;
            stateMachine.canLook = true;
        }

        public override void OnUpdate(float deltaTime)
        {
            if (!Input.GetMouseButtonDown(0))
            {
                input = playerInputController.moveVector.ToVector3XZ();
                Look(deltaTime);
                return;
            }
            
            stateMachine.DisableShotGunRange();
            playerAnimateController.PlayFireShotGun();
            playerAnimateController.SetLayerWeight(1,1);
            stateMachine.TriggerCooldown(SkillType.Shotgun);
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