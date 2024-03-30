using _GAME_.Scripts.GlobalVariables;
using JetBrains.Annotations;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerAnimateController : Bear
    {
        #region Private Variables

        private Animator _animator;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveZ = Animator.StringToHash("MoveZ");

        private Vector2 _currentDirection = Vector2.zero;
        private static readonly int DieKey = Animator.StringToHash("Die");
        private static readonly int ShootKey = Animator.StringToHash("Shoot");

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
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

        public void Die()
        {
            _animator.SetTrigger(DieKey);
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

        #endregion
    }
}