using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using _GAME_.Scripts.Player;
using UnityEngine;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class GiveDamageNode : Node
    {
        #region Private Variables

        private Transform _lastTarget;
        private PlayerHealthController _playerHealthController;
        private EnemyHealthController _enemyHealthController;

        private float _attackTime = 1;
        private float _attackCounter;

        #endregion

        #region Constructor

        public GiveDamageNode(EnemyHealthController enemyHealthController)
        {
            _enemyHealthController = enemyHealthController;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (!GameManager.Instance.IsGameStarted)
            {
                state = NodeState.FAILURE;
                return state;
            }

            if (_enemyHealthController.IsDead)
            {
                state = NodeState.FAILURE;
                return state;
            }

            Transform target = (Transform)GetData(DataContextKey.TARGET);

            if (target != _lastTarget)
            {
                _playerHealthController = target.GetComponent<PlayerHealthController>();
                _lastTarget = target;
            }

            _attackCounter += Time.deltaTime;

            if (_attackCounter >= _attackTime)
            {
                bool playerIsDead = _playerHealthController.TakeHit();

                if (playerIsDead)
                {
                    ClearData(DataContextKey.TARGET);
                }
                else
                {
                    _attackCounter = 0;
                }
            }

            state = NodeState.RUNNING;
            return state;
        }

        #endregion
    }
}