using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerAnimateController : MonoBehaviour
    {
        #region Private Variables

        private Animator _animator;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveZ = Animator.StringToHash("MoveZ");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private Vector2 _currentDirection = Vector2.zero;
        private static readonly int DieKey = Animator.StringToHash("Die");

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
            float threshold = 0.707107f;
            float difference = 0.001f;

            Vector2 targetDirection = direction;

            if ((Mathf.Abs(direction.x) > (threshold - difference) &&
                 Mathf.Abs(direction.x) < (threshold + difference)) &&
                (Mathf.Abs(direction.y) > (threshold - difference) &&
                 Mathf.Abs(direction.y) < (threshold + difference)))
            {
                targetDirection.x = Mathf.Sign(direction.x);
                targetDirection.y = Mathf.Sign(direction.y);
            }

            if (direction.y < 0)
            {
                targetDirection.x *= -1;
                _animator.SetFloat(Speed, -1);
            }
            else
            {
                _animator.SetFloat(Speed, 1);
            }
            
            _currentDirection.x = Mathf.Lerp(_currentDirection.x, targetDirection.x, 15f * Time.deltaTime);
            _currentDirection.y = Mathf.Lerp(_currentDirection.y, targetDirection.y, 15f * Time.deltaTime);

            _animator.SetFloat(MoveX, _currentDirection.x);
            _animator.SetFloat(MoveZ, _currentDirection.y);
        }

        public void Die()
        {
            _animator.SetTrigger(DieKey);
        }

        #endregion
    }
}