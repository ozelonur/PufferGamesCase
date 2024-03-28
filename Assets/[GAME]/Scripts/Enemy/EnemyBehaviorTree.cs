using System.Collections.Generic;
using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.Enemy.Nodes;
using UnityEngine;
using UnityEngine.AI;
using Tree = _GAME_.Scripts.Core.BehaviorTree.Tree;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyBehaviorTree : Tree
    {
        #region Serialized Fields

        [Header("Configurations")] [SerializeField]
        private LayerMask layerMask;

        #endregion

        #region Public Variables

        public Transform[] waypoints;
        public static float speed = 2;
        public static float fovRange = 10;
        public static float damageRange = 1f;

        #endregion

        #region Private Variables

        private EnemyAnimateController _enemyAnimateController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region MonoBehaviour Methods

        private void Awake()
        {
            _enemyAnimateController = transform.GetChild(0).GetChild(0).GetComponent<EnemyAnimateController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        #endregion

        #region Inherit Methods

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Sequence
                (
                    new List<Node>
                    {
                        new CheckPlayerInDamageRangeNode(transform, _enemyAnimateController),
                        new GiveDamageNode()
                    }),
                new Sequence
                (
                    new List<Node>
                    {
                        new CheckPlayerInFOVRangeNode(transform, layerMask),
                        new GoToTargetNode(transform,_enemyAnimateController, _navMeshAgent)
                    }),
                new PatrolNode(transform, waypoints, _enemyAnimateController, _navMeshAgent)
            });

            return root;
        }

        #endregion
    }
}