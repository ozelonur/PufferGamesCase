using _GAME_.Scripts.GlobalVariables;
using JetBrains.Annotations;
using OrangeBear.EventSystem;
using UnityEngine;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyAnimateController : Bear
    {
        #region Serialized Fields

        [Header("Configurations")] [SerializeField]
        private EnemyType type;

        #endregion

        #region Private Variables

        private Animator _animator;
        private EnemyBehaviorTree _enemyBehaviorTree;
        private static readonly int WalkKey = Animator.StringToHash("Walk");
        private static readonly int DieKey = Animator.StringToHash("Die");
        private static readonly int SlipKey = Animator.StringToHash("Slip");
        private static readonly int WalkForward = Animator.StringToHash("WalkForward");
        private static readonly int GetHitFront = Animator.StringToHash("Get Hit Front");
        private static readonly int StunnedLoop = Animator.StringToHash("Stunned Loop");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Idle = Animator.StringToHash("Idle");

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
            if (type == EnemyType.Robot)
            {
                _animator.SetBool(WalkKey, status);
            }

            else
            {
                if (!status)
                {
                    _animator.SetBool(Idle, true);
                    _animator.SetBool(WalkForward, false);
                }

                else
                {
                    _animator.SetBool(Idle, false);
                    _animator.SetBool(WalkForward, true);
                }
            }
        }

        public void GetHit()
        {
            if (type == EnemyType.Robot)
            {
                int animationsCount = System.Enum.GetValues(typeof(EnemyHitAnimations)).Length;

                int randomIndex = Random.Range(0, animationsCount);

                EnemyHitAnimations selectedAnimation = (EnemyHitAnimations)randomIndex;

                _animator.SetTrigger(selectedAnimation.ToString());
            }

            else
            {
                _animator.SetTrigger(GetHitFront);
            }
        }

        public void Slip()
        {
            if (type == EnemyType.Robot)
            {
                _animator.SetBool(WalkKey, false);
                _animator.SetTrigger(SlipKey);
            }

            else
            {
                _animator.SetBool(WalkForward,false);
                _animator.SetBool(Idle,false);
                _animator.SetBool(StunnedLoop, true);
            }
        }

        public void Die()
        {
            if (type == EnemyType.Robot)
            {
                _animator.SetTrigger(DieKey);
            }
            else
            {
                _animator.SetBool(WalkForward,false);
                _animator.SetBool(Idle,false);
                _animator.SetTrigger(Death);
            }
        }

        public void SetIdle()
        {
            if (type != EnemyType.Robot)
            {
                _animator.SetBool(Idle, true);
            }
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
            if (type != EnemyType.Robot)
            {
                _animator.SetBool(WalkForward,false);
                _animator.SetBool(Idle,false);
            }
            _enemyBehaviorTree.isGettingHit = true;
        }

        [UsedImplicitly]
        private void GetHitStopped()
        {
            Debug.Log("Get Hit stopped!");
            _enemyBehaviorTree.isGettingHit = false;
            if (type != EnemyType.Robot)
            {
                _animator.SetBool(Idle,true);
            }
        }

        [UsedImplicitly]
        private void EndSlip()
        {
            if (type == EnemyType.Robot)
            {
                _enemyBehaviorTree.isSlipping = false;
            }
        }

        #endregion
    }
}