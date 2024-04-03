using _GAME_.Scripts.Core.BehaviorTree;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class SlipNode : Node
    {
        #region Private Variables

        private NavMeshAgent _navMeshAgent;
        private EnemyBehaviorTree _enemyBehaviorTree;
        private float _speed;

        #endregion

        #region Constructor

        public SlipNode(EnemyBehaviorTree enemyBehaviorTree, NavMeshAgent navMeshAgent)
        {
            _enemyBehaviorTree = enemyBehaviorTree;
            _navMeshAgent = navMeshAgent;
            _speed = _navMeshAgent.speed;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_enemyBehaviorTree.isSlipping)
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