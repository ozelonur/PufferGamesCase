using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.GlobalVariables;
using UnityEngine;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class CheckPlayerInFOVRangeNode : Node
    {
        #region Private Variables

        private Transform _transform;
        private LayerMask _layerMask;
        private EnemyAnimateController _enemyAnimateController;

        #endregion

        #region Constructor

        public CheckPlayerInFOVRangeNode(Transform transform, LayerMask layerMask, EnemyAnimateController enemyAnimateController)
        {
            _transform = transform;
            _layerMask = layerMask;
            _enemyAnimateController = enemyAnimateController;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            object t = GetData(DataContextKey.TARGET);

            if (t == null)
            {
                Collider[] colliders =
                    Physics.OverlapSphere(_transform.position, EnemyBehaviorTree.fovRange, _layerMask);

                if (colliders.Length > 0)
                {
                    parent.parent.SetData(DataContextKey.TARGET, colliders[0].transform);
                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }
        
        

        #endregion
    }
}