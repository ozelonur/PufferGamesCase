using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class PatrolNode : Node
    {
        #region Private Variables

        private Transform _transform;
        private Transform[] _waypoints;

        private EnemyAnimateController _enemyAnimateController;
        private EnemyHealthController _enemyHealthController;
        private NavMeshAgent _navMeshAgent;

        private int _currentWaypointIndex;

        private float _waitTime = 1f;
        private float _waitCounter;
        private bool _isWaiting;
        private float _speed;

        #endregion

        #region Constructor

        public PatrolNode(Transform transform, Transform[] waypoints, EnemyAnimateController enemyAnimateController,
            NavMeshAgent navMeshAgent, EnemyHealthController enemyHealthController)
        {
            _transform = transform;
            _waypoints = waypoints;
            _enemyAnimateController = enemyAnimateController;
            _navMeshAgent = navMeshAgent;
            _speed = _navMeshAgent.speed;
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

            if (_isWaiting)
            {
                _waitCounter += Time.deltaTime;

                if (_waitCounter >= _waitTime)
                {
                    _isWaiting = false;
                    _enemyAnimateController.Walk(true);
                    _navMeshAgent.speed = _speed;
                }
            }

            else
            {
                Transform waypoint = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(_transform.position, waypoint.position) < .1f)
                {
                    _transform.position = waypoint.position;
                    _waitCounter = 0;
                    _isWaiting = true;
                    _enemyAnimateController.Walk(false);

                    _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                }

                else
                {
                    _enemyAnimateController.Walk(true);
                    _navMeshAgent.enabled = true;
                    _navMeshAgent.speed = _speed;
                    _navMeshAgent.SetDestination(waypoint.position);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }

        #endregion
    }
}