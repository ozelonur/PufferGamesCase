using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class CheckPlayerInDamageRangeNode : Node
    {
        #region Private Variables

        private Transform _transform;
        private EnemyAnimateController _enemyAnimateController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region Constructor

        public CheckPlayerInDamageRangeNode(Transform transform, EnemyAnimateController enemyAnimateController,
            NavMeshAgent navMeshAgent)
        {
            _transform = transform;
            _enemyAnimateController = enemyAnimateController;
            _navMeshAgent = navMeshAgent;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            object t = GetData(DataContextKey.TARGET);

            if (t == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            Transform target = (Transform)t;

            if (Vector3.Distance(_transform.position, target.position) <= EnemyBehaviorTree.damageRange)
            {
                _enemyAnimateController.Walk(false);
                _navMeshAgent.speed = 0;
                _navMeshAgent.enabled = false;
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        #endregion
    }
}