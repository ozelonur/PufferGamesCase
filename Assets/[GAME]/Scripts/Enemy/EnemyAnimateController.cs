using _GAME_.Scripts.GlobalVariables;
using JetBrains.Annotations;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyAnimateController : Bear
    {
        #region Private Variables

        private Animator _animator;
        private EnemyBehaviorTree _enemyBehaviorTree;
        private static readonly int WalkKey = Animator.StringToHash("Walk");
        private static readonly int DieKey = Animator.StringToHash("Die");

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

        public void GetHit()
        {
            int animationsCount = System.Enum.GetValues(typeof(EnemyHitAnimations)).Length;
        
            int randomIndex = Random.Range(0, animationsCount);
        
            EnemyHitAnimations selectedAnimation = (EnemyHitAnimations)randomIndex;
        
            _animator.SetTrigger(selectedAnimation.ToString());
        }

        public void Die()
        {
            _animator.SetTrigger(DieKey);
        }

        public void SetBehaviourTree(EnemyBehaviorTree enemy)
        {
            _enemyBehaviorTree = enemy;
        }

        #endregion

        #region Private Methods

        [UsedImplicitly]
        private void GetHitStarted()
        {
            _enemyBehaviorTree.isGettingHit = true;
        }

        [UsedImplicitly]
        private void GetHitStopped()
        {
            _enemyBehaviorTree.isGettingHit = false;
        }

        #endregion
    }
}