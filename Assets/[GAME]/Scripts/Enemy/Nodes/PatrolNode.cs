using _GAME_.Scripts.Core.BehaviorTree;
using UnityEngine;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class PatrolNode : Node
    {
        #region Private Variables

        private Transform _transform;
        private Transform[] _waypoints;

        private EnemyAnimateController _enemyAnimateController;

        private int _currentWaypointIndex;

        private float _waitTime = 1f;
        private float _waitCounter;
        private bool _isWaiting;

        #endregion

        #region Constructor

        public PatrolNode(Transform transform, Transform[] waypoints, EnemyAnimateController enemyAnimateController)
        {
            _transform = transform;
            _waypoints = waypoints;
            _enemyAnimateController = enemyAnimateController;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_isWaiting)
            {
                _waitCounter += Time.deltaTime;

                if (_waitCounter >= _waitTime)
                {
                    _isWaiting = false;
                    _enemyAnimateController.Walk(true);
                }
            }

            else
            {
                Transform waypoint = _waypoints[_currentWaypointIndex];
                if (Vector3.Distance(_transform.position, waypoint.position) < .01f)
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
                    _transform.position =
                        Vector3.MoveTowards(_transform.position, waypoint.position, EnemyBehaviorTree.speed * Time.deltaTime);
                    _transform.LookAt(waypoint.position);
                }
            }

            state = NodeState.RUNNING;
            return state;
        }

        #endregion
    }
}