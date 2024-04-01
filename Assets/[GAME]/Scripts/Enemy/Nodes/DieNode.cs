using _GAME_.Scripts.Core.BehaviorTree;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class DieNode : Node
    {
        #region Private Variables

        private EnemyHealthController _enemyHealthController;
        private EnemyAnimateController _enemyAnimateController;
        private EnemyDissolveController _enemyDissolveController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region Constructor

        public DieNode(EnemyHealthController enemyHealthController, NavMeshAgent navMeshAgent,
            EnemyAnimateController enemyAnimateController, EnemyDissolveController enemyDissolveController)
        {
            _enemyHealthController = enemyHealthController;
            _enemyAnimateController = enemyAnimateController;
            _navMeshAgent = navMeshAgent;
            _enemyDissolveController = enemyDissolveController;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_enemyHealthController.IsDead)
            {
                _navMeshAgent.speed = 0;
                _enemyAnimateController.Die();
                _enemyDissolveController.DissolveMesh(1.5f);
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