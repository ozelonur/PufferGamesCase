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
        public static float fovRange = 2.5f;
        public static float damageRange = 1f;

        public bool isGettingHit;

        #endregion

        #region Private Variables

        private EnemyAnimateController _enemyAnimateController;
        private EnemyHealthController _enemyHealthController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region MonoBehaviour Methods

        protected void Awake()
        {
            _enemyAnimateController = transform.GetChild(0).GetChild(0).GetComponent<EnemyAnimateController>();
            _enemyHealthController = GetComponent<EnemyHealthController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyAnimateController.SetBehaviourTree(this);
        }

        #endregion

        #region Inherit Methods

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new DieNode(_enemyHealthController, _navMeshAgent, _enemyAnimateController),
                new ReceiveDamageNode(_enemyAnimateController, _enemyHealthController, _navMeshAgent),
                new ReceivingDamageNode(this, _navMeshAgent),
                new Sequence
                (
                    new List<Node>
                    {
                        new CheckPlayerInDamageRangeNode(transform, _enemyAnimateController, _navMeshAgent,
                            _enemyHealthController),
                        new GiveDamageNode(_enemyHealthController)
                    }),
                new Sequence
                (
                    new List<Node>
                    {
                        new CheckPlayerInFOVRangeNode(transform, layerMask, _enemyHealthController),
                        new GoToTargetNode(transform, _enemyAnimateController, _navMeshAgent, _enemyHealthController)
                    }),
                new PatrolNode(transform, waypoints, _enemyAnimateController, _navMeshAgent, _enemyHealthController)
            });

            return root;
        }

        #endregion
    }
}