using _GAME_.Scripts.Core.StateMachine;
using _GAME_.Scripts.Player.States;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerStateMachine : StateMachineBase
    {
        #region Public Variables

        public Rigidbody playerRigidBody;
        public float speed;
        public float turnSpeed = 360;
        public bool canLook;

        [HideInInspector] public PlayerInputController inputController;
        [HideInInspector] public PlayerAnimateController playerAnimateController;
        [HideInInspector] public Transform playerMoveTransform;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody>();
            playerMoveTransform = transform;
            inputController = GetComponent<PlayerInputController>();
            playerAnimateController =
                playerMoveTransform.GetChild(0).GetChild(0).GetComponent<PlayerAnimateController>();
        }

        private void Start()
        {
            SwitchState(new PlayerMoveState(this));
        }

        #endregion
    }
}