using _GAME_.Scripts.Core.BehaviorTree;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class ReceivingDamageNode : Node
    {
        #region Private Variables

        private NavMeshAgent _navMeshAgent;
        private EnemyBehaviorTree _enemyBehaviorTree;
        private float _speed;

        #endregion

        #region Constructor

        public ReceivingDamageNode(EnemyBehaviorTree enemyBehaviorTree, NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _speed = _navMeshAgent.speed;
            _enemyBehaviorTree = enemyBehaviorTree;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_enemyBehaviorTree.isGettingHit)
            {
                _navMeshAgent.speed = 0;
                state = NodeState.RUNNING;
            }
            else
            {
                _navMeshAgent.speed = _speed;
                state = NodeState.FAILURE;
            }

            return state;
        }

        #endregion
    }
}