using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Player.Bullet;
using DitzelGames.FastIK;
using JetBrains.Annotations;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerAnimateController : Bear
    {
        #region Serialized Fields

        [Header("Components")] [SerializeField]
        private FastIKFabric ik;

        [SerializeField] private Transform handTarget;
        [SerializeField] private Transform magazineTarget;

        #endregion
        #region Private Variables

        private Animator _animator;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveZ = Animator.StringToHash("MoveZ");

        private Vector2 _currentDirection = Vector2.zero;
        private static readonly int DieKey = Animator.StringToHash("Die");
        private static readonly int ShootKey = Animator.StringToHash("Shoot");
        private static readonly int IdlingShootKey = Animator.StringToHash("IdlingShoot");
        private static readonly int ReloadKey = Animator.StringToHash("Reload");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        private static readonly int ThrowGrenadeKey = Animator.StringToHash("ThrowGrenade");

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            ik.Target = handTarget;
            _animator = GetComponent<Animator>();
        }

        #endregion

        #region Public Methods

        public void Move(Vector2 direction)
        {
            _currentDirection.x = Mathf.Lerp(_currentDirection.x, direction.x, 15f * Time.deltaTime);
            _currentDirection.y = Mathf.Lerp(_currentDirection.y, direction.y, 15f * Time.deltaTime);

            _animator.SetFloat(MoveX, _currentDirection.x);
            _animator.SetFloat(MoveZ, _currentDirection.y);
        }

        public void Shoot(bool status)
        {
            _animator.SetBool(ShootKey, status);
        }

        public void IdlingShoot(bool status)
        {
            _animator.SetBool(IdlingShootKey, status);
        }

        public void Die()
        {
            _animator.SetTrigger(DieKey);
        }

        public void ThrowGrenade()
        {
            _animator.SetBool(ThrowGrenadeKey, true);
        }

        public void Reload()
        {
            Roar(CustomEvents.Reload);
            ik.enabled = false;
            _animator.SetTrigger(ReloadKey);
        }

        public void Jump()
        {
            _animator.SetTrigger(JumpKey);
        }

        public void SetLayerWeight(int index, float weight)
        {
            _animator.SetLayerWeight(index, weight);
        }

        #endregion

        #region Private Methods

        [UsedImplicitly]
        private void Shot()
        {
            Roar(CustomEvents.Shot);
        }

        [UsedImplicitly]
        private void ChangeIKTarget()
        {
            ik.enabled = true;
            ik.Target = magazineTarget;
        }

        [UsedImplicitly]
        private void ResetIKTarget()
        {
            ik.enabled = false;
            ik.Target = handTarget;
        }

        [UsedImplicitly]
        private void DisableIK()
        {
            ik.enabled = false;
        }

        [UsedImplicitly]
        private void EnableIK()
        {
            ik.enabled = true;
        }

        [UsedImplicitly]
        private void ResetLayer()
        {
            ik.enabled = true;
            _animator.SetLayerWeight(1,0);
        }

        [UsedImplicitly]
        private void DropMagazine()
        {
            Roar(CustomEvents.DropMagazine);
        }

        [UsedImplicitly]
        private void SpawnMagazine(MagazineType type)
        {
            Roar(CustomEvents.GenerateMagazine, type);
        }

        [UsedImplicitly]
        private void InsertMagazine()
        {
            Roar(CustomEvents.InsertMagazine);
        }

        [UsedImplicitly]
        private void RightToLeft()
        {
            ik.enabled = false;
            Roar(CustomEvents.WeaponPassTheLeftHand);
        }

        [UsedImplicitly]
        private void LeftToRight()
        {
            _animator.SetBool(ThrowGrenadeKey, false);
            Roar(CustomEvents.AbortThrowingGrenade);
            ik.enabled = true;
            Roar(CustomEvents.WeaponPassTheRightHand);
            SetLayerWeight(1,0);
        }

        [UsedImplicitly]
        private void SpawnGrenade()
        {
            Roar(CustomEvents.SpawnGrenade);
        }

        [UsedImplicitly]
        private void ThrowGrenadeToTarget()
        {
            Roar(CustomEvents.ThrowGrenadeToTarget);
        }

        #endregion
    }
}