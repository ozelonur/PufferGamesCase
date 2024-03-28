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

            if ((Mathf.Abs(direction.x) > (threshold - difference) &&
                 Mathf.Abs(direction.x) < (threshold + difference)) &&
                (Mathf.Abs(direction.y) > (threshold - difference) &&
                 Mathf.Abs(direction.y) < (threshold + difference)))
            {
                direction.x = Mathf.Sign(direction.x);
                direction.y = Mathf.Sign(direction.y);
            }

            if (direction.y < 0)
            {
                direction.x *= -1;
                _animator.SetFloat(Speed, -1);
            }
            else
            {
                _animator.SetFloat(Speed, 1);
            }

            _animator.SetFloat(MoveX, direction.x);
            _animator.SetFloat(MoveZ, direction.y);
        }

        #endregion
    }
}