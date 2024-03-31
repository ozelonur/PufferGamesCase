using _GAME_.Scripts.Core.StateMachine;
using _GAME_.Scripts.GlobalVariables;
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
        public float mouseSensitivity = 50;
        public float visionRadius = 5;
        public bool canLook;
        public LayerMask targetLayerMask;

        [HideInInspector] public PlayerInputController inputController;
        [HideInInspector] public PlayerAnimateController playerAnimateController;
        [HideInInspector] public Transform playerMoveTransform;
        [HideInInspector] public Transform playerRotateTransform;
        [SerializeField] public Transform weaponTransform;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody>();
            playerMoveTransform = transform;
            playerRotateTransform = playerMoveTransform.GetChild(0);
            inputController = GetComponent<PlayerInputController>();
            playerAnimateController =
                playerMoveTransform.GetChild(0).GetChild(0).GetComponent<PlayerAnimateController>();
        }

        private void Start()
        {
            Roar(CustomEvents.SetPlayerTransform, playerMoveTransform);
        }

        #endregion

        #region Event Methods

        protected override void CheckRoarings(bool status)
        {
            if (status)
            {
                Register(CustomEvents.OnGameStart, OnGameStart);
            }

            else
            {
                Unregister(CustomEvents.OnGameStart, OnGameStart);
            }
        }

        private void OnGameStart(object[] arguments)
        {
            SwitchState(new PlayerMoveState(this));
        }

        #endregion
    }
}