using _GAME_.Scripts.Core.StateMachine;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Player.States;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerStateMachine : StateMachineBase
    {
        #region Serialized Fields

        [Header("Configurations")] [SerializeField]
        private float dashSpeed;

        [SerializeField] private float dashDistance;

        #endregion

        #region Public Variables

        public Rigidbody playerRigidBody;
        public float speed;
        public float turnSpeed = 360;
        public float mouseSensitivity = 50;
        public float visionRadius = 5;
        public bool canLook;
        public LayerMask targetLayerMask;
        public int magazineCapacity = 30;

        [HideInInspector] public PlayerInputController inputController;
        [HideInInspector] public PlayerAnimateController playerAnimateController;
        [HideInInspector] public Transform playerMoveTransform;
        [HideInInspector] public Transform playerRotateTransform;
        [SerializeField] public Transform weaponTransform;
        [SerializeField] public int currentBulletCount;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            currentBulletCount = magazineCapacity;
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
                Register(CustomEvents.Shot, Shot);
                Register(CustomEvents.Reload, Reload);
                Register(CustomEvents.Dash, Dash);
            }

            else
            {
                Unregister(CustomEvents.OnGameStart, OnGameStart);
                Unregister(CustomEvents.Shot, Shot);
                Unregister(CustomEvents.Reload, Reload);
                Unregister(CustomEvents.Dash, Dash);
            }
        }

        private void Dash(object[] arguments)
        {
            SwitchState(new PlayerDashState(this, dashSpeed, dashDistance));
        }

        private void Reload(object[] arguments)
        {
            currentBulletCount = magazineCapacity;
        }

        private void Shot(object[] arguments)
        {
            if (currentBulletCount <= 0)
            {
                playerAnimateController.SetLayerWeight(1, 1);
                playerAnimateController.Reload();
                return;
            }

            currentBulletCount--;
        }

        private void OnGameStart(object[] arguments)
        {
            SwitchState(new PlayerMoveState(this));
        }

        #endregion
    }
}