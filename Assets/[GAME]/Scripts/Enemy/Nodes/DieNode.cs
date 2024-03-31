using _GAME_.Scripts.Core.BehaviorTree;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class DieNode : Node
    {
        #region Private Variables

        private EnemyHealthController _enemyHealthController;
        private EnemyAnimateController _enemyAnimateController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region Constructor

        public DieNode(EnemyHealthController enemyHealthController, NavMeshAgent navMeshAgent, EnemyAnimateController enemyAnimateController)
        {
            _enemyHealthController = enemyHealthController;
            _enemyAnimateController = enemyAnimateController;
            _navMeshAgent = navMeshAgent;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_enemyHealthController.IsDead)
            {
                _navMeshAgent.speed = 0;
                _enemyAnimateController.Die();
                state = NodeState.RUNNING;
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