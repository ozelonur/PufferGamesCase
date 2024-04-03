using _GAME_.Scripts.Core.BehaviorTree;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class ReceiveDamageNode : Node
    {
        #region Private Variables

        private EnemyAnimateController _enemyAnimateController;
        private EnemyHealthController _enemyHealthController;
        private EnemyBehaviorTree _enemyBehaviorTree;
        private NavMeshAgent _navMeshAgent;
        #endregion

        #region Constructor

        public ReceiveDamageNode(EnemyAnimateController enemyAnimateController,
            EnemyHealthController enemyHealthController, NavMeshAgent navMeshAgent, EnemyBehaviorTree enemyBehaviorTree)
        {
            _enemyAnimateController = enemyAnimateController;
            _navMeshAgent = navMeshAgent;
            _enemyHealthController = enemyHealthController;
            _enemyBehaviorTree = enemyBehaviorTree;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_enemyHealthController.IsDamaged)
            {
                _navMeshAgent.speed = 0;
                if (!_enemyBehaviorTree.isSlipping)
                {
                    _enemyAnimateController.GetHit();
                }
                _enemyHealthController.IsDamaged = false;
                state = NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.FAILURE;
            }

            return state;
        }

        #endregion
    }
}