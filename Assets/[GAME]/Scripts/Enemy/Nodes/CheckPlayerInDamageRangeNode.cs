using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class CheckPlayerInDamageRangeNode : Node
    {
        #region Private Variables

        private Transform _transform;
        private EnemyAnimateController _enemyAnimateController;

        #endregion

        #region Constructor

        public CheckPlayerInDamageRangeNode(Transform transform, EnemyAnimateController enemyAnimateController)
        {
            _transform = transform;
            _enemyAnimateController = enemyAnimateController;
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
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        #endregion
    }
}