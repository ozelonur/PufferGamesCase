using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyAnimateController : Bear
    {
        #region Private Variables

        private Animator _animator;
        private static readonly int WalkKey = Animator.StringToHash("Walk");

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        #endregion

        #region Public Methods

        public void Walk(bool status)
        {
            _animator.SetBool(WalkKey, status);
        }

        #endregion
    }
}