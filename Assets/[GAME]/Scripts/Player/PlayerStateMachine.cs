using _GAME_.Scripts.Core.StateMachine;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.Player.States;
using DG.Tweening;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerStateMachine : StateMachineBase
    {
        #region Public Variables

        public Rigidbody playerRigidBody;
        public bool canLook;
        public LayerMask targetLayerMask;

        [HideInInspector] public PlayerInputController inputController;
        [HideInInspector] public PlayerAnimateController playerAnimateController;
        [HideInInspector] public PlayerMeshTrailController playerMeshTrailController;
        [HideInInspector] public Projection projection;
        [HideInInspector] public Transform playerMoveTransform;
        [HideInInspector] public Transform playerRotateTransform;
        [SerializeField] public Transform weaponTransform;
        [SerializeField] public int currentBulletCount;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] public GameObject weapon;
        [SerializeField] public GameObject shotGun;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            currentBulletCount = DataManager.Instance.GetPlayerBaseAttackData().magazineCapacity;
            playerRigidBody = GetComponent<Rigidbody>();
            playerMoveTransform = transform;
            playerRotateTransform = playerMoveTransform.GetChild(0);
            inputController = GetComponent<PlayerInputController>();
            playerMeshTrailController = GetComponent<PlayerMeshTrailController>();
            playerAnimateController =
                playerMoveTransform.GetChild(0).GetChild(0).GetComponent<PlayerAnimateController>();
            projection = GetComponent<Projection>();
        }

        private void Start()
        {
            PlayerManager.Instance.SetPlayer(this);
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
                
                Register(CustomEvents.ThrowGrenade, ThrowGrenade);
                Register(CustomEvents.SpawnGrenade, SpawnGrenade);
                Register(CustomEvents.ThrowGrenadeToTarget, ThrowGrenadeToTarget);
                Register(CustomEvents.AbortThrowingGrenade, AbortThrowingGrenade);
                
                Register(CustomEvents.ThrowOilBomb, ThrowOilBomb);
                Register(CustomEvents.SpawnOilBomb, SpawnOilBomb);
                Register(CustomEvents.ThrowOilBombToTarget, ThrowOilBombToTarget);
                Register(CustomEvents.AbortThrowingOilBomb, AbortThrowingOilBomb);
                
                
                Register(CustomEvents.EnableShotgun, EnableShotgun);
                Register(CustomEvents.DisableShotGun, DisableShotGun);
            }

            else
            {
                Unregister(CustomEvents.OnGameStart, OnGameStart);
                Unregister(CustomEvents.Shot, Shot);
                Unregister(CustomEvents.Reload, Reload);
                Unregister(CustomEvents.Dash, Dash);
                Unregister(CustomEvents.ThrowGrenade, ThrowGrenade);
                Unregister(CustomEvents.SpawnGrenade, SpawnGrenade);
                Unregister(CustomEvents.ThrowGrenadeToTarget, ThrowGrenadeToTarget);
                Unregister(CustomEvents.AbortThrowingGrenade, AbortThrowingGrenade);
                
                Register(CustomEvents.ThrowOilBomb, ThrowOilBomb);
                Register(CustomEvents.SpawnOilBomb, SpawnOilBomb);
                Register(CustomEvents.ThrowOilBombToTarget, ThrowOilBombToTarget);
                Register(CustomEvents.AbortThrowingOilBomb, AbortThrowingOilBomb);
                
                Unregister(CustomEvents.EnableShotgun, EnableShotgun);
                Unregister(CustomEvents.DisableShotGun, DisableShotGun);
            }
        }

        private void AbortThrowingOilBomb(object[] arguments)
        {
            SwitchState(new PlayerMoveState(this));
        }

        private void ThrowOilBombToTarget(object[] arguments)
        {
            projection.ThrowOilBombToTarget();
        }

        private void SpawnOilBomb(object[] arguments)
        {
            projection.SpawnOilBomb();
        }

        private void ThrowOilBomb(object[] arguments)
        {
            SwitchState(new PlayerOilBombState(this, groundLayer));
        }

        private void DisableShotGun(object[] arguments)
        {
            weapon.SetActive(true);
            shotGun.SetActive(false);
            SwitchState(new PlayerMoveState(this));
        }

        private void EnableShotgun(object[] arguments)
        {
            weapon.SetActive(false);
            shotGun.SetActive(true);
            SwitchState(new PlayerShotgunState(this));
        }

        private void AbortThrowingGrenade(object[] arguments)
        {
            SwitchState(new PlayerMoveState(this));
        }

        private void ThrowGrenadeToTarget(object[] arguments)
        {
            projection.ThrowGrenadeToTarget();
        }

        private void SpawnGrenade(object[] arguments)
        {
            projection.SpawnGrenade();
        }

        private void ThrowGrenade(object[] arguments)
        {
            SwitchState(new PlayerGrenadeState(this, groundLayer));
        }

        private void Dash(object[] arguments)
        {
            Roar(CustomEvents.StartCoolDown, SkillType.Dash);
            playerAnimateController.Jump();
            playerMeshTrailController.ActivateTrail();
            DOVirtual.DelayedCall(.05f, () => { SwitchState(new PlayerDashState(this)); });
        }

        private void Reload(object[] arguments)
        {
            currentBulletCount = DataManager.Instance.GetPlayerBaseAttackData().magazineCapacity;
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

        #region Public Methods

        public void DisableShotGunRange()
        {
            Roar(CustomEvents.DisableShotGunRange);
        }

        public void TriggerCooldown(SkillType skillType)
        {
            Roar(CustomEvents.StartCoolDown, skillType);
        }

        #endregion
    }
}