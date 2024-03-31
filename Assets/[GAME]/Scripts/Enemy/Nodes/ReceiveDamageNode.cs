using System.Threading.Tasks;
using _GAME_.Scripts.Core.BehaviorTree;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class ReceiveDamageNode : Node
    {
        #region Private Variables

        private EnemyAnimateController _enemyAnimateController;
        private EnemyHealthController _enemyHealthController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region Constructor

        public ReceiveDamageNode(EnemyAnimateController enemyAnimateController,
            EnemyHealthController enemyHealthController, NavMeshAgent navMeshAgent)
        {
            _enemyAnimateController = enemyAnimateController;
            _navMeshAgent = navMeshAgent;
            _enemyHealthController = enemyHealthController;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_enemyHealthController.IsDamaged)
            {
                _navMeshAgent.isStopped = true;
                _enemyAnimateController.GetHit();
                Task.Delay(500).ContinueWith(t => _navMeshAgent.isStopped = false);
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