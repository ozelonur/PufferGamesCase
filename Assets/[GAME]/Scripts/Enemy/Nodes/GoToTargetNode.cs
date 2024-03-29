using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class GoToTargetNode : Node
    {
        #region Private Variables

        private Transform _transform;
        private Vector3 _startPosition = Vector3.zero;
        private EnemyAnimateController _enemyAnimateController;
        private NavMeshAgent _navMeshAgent;
        private float _speed;

        #endregion

        #region Constructor

        public GoToTargetNode(Transform transform, EnemyAnimateController enemyAnimateController,
            NavMeshAgent navMeshAgent)
        {
            _transform = transform;
            _enemyAnimateController = enemyAnimateController;
            _navMeshAgent = navMeshAgent;
            _speed = _navMeshAgent.speed;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            Transform target = (Transform)GetData(DataContextKey.TARGET);

            if (_startPosition == Vector3.zero)
            {
                _startPosition = _transform.position;
            }

            float distance = Vector3.Distance(_startPosition, _transform.position);

            if (distance >= 20)
            {
                _startPosition = Vector3.zero;
                ClearData(DataContextKey.TARGET);
            }

            if (Vector3.Distance(_transform.position, target.position) > 1f)
            {
                _navMeshAgent.enabled = true;
                _navMeshAgent.speed = _speed;
                _enemyAnimateController.Walk(true);
                _navMeshAgent.SetDestination(target.position);
            }
            else
            {
                _navMeshAgent.speed = 0;
                _navMeshAgent.enabled = false;
            }

            state = NodeState.RUNNING;
            return state;
        }

        #endregion
    }
}