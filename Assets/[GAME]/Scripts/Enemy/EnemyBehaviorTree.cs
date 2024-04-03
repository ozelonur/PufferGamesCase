using System.Collections.Generic;
using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.Enemy.Nodes;
using _GAME_.Scripts.Extensions;
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

        [SerializeField] private float patrolPointsCircleRadius;
        [SerializeField] private int patrolPointMinCount;
        [SerializeField] private int patrolPointMaxCount;
        [SerializeField] private float minWaitTime;
        [SerializeField] private float maxWaitTime;

        #endregion

        #region Public Variables

        public Transform[] waypoints;
        public static float fovRange = 2.5f;
        public static float damageRange = 1f;

        public bool isGettingHit;
        public bool isSlipping;
        

        #endregion

        #region Private Variables

        private EnemyAnimateController _enemyAnimateController;
        private EnemyHealthController _enemyHealthController;
        private EnemyDissolveController _enemyDissolveController;
        private NavMeshAgent _navMeshAgent;

        #endregion

        #region MonoBehaviour Methods

        protected void Awake()
        {
            _enemyAnimateController = transform.GetChild(0).GetChild(0).GetComponent<EnemyAnimateController>();
            _enemyHealthController = GetComponent<EnemyHealthController>();
            _enemyDissolveController = GetComponent<EnemyDissolveController>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void Start()
        {
            waypoints = GeneratePatrolPoints(RandomExtensions.GetRandom(patrolPointMinCount, patrolPointMaxCount),
                patrolPointsCircleRadius);

            _enemyAnimateController.SetBehaviourTree(this);
            base.Start();
        }

        #endregion

        #region Inherit Methods

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new DieNode(_enemyHealthController, _navMeshAgent, _enemyAnimateController, _enemyDissolveController),
                new ReceiveDamageNode(_enemyAnimateController, _enemyHealthController, _navMeshAgent, this),
                new ReceivingDamageNode(this, _navMeshAgent),
                new SlipNode(this, _navMeshAgent),
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
                new PatrolNode(transform, waypoints, _enemyAnimateController, _navMeshAgent, _enemyHealthController, this)
            });

            return root;
        }

        #endregion

        #region Public Methods

        public void Slip()
        {
            isSlipping = true;
            _enemyAnimateController.Slip();
        }

        public float GetWaitTime()
        {
            return RandomExtensions.GetRandom(minWaitTime, maxWaitTime);
        }

        #endregion

        #region Private Methods

        private Transform[] GeneratePatrolPoints(int wayPointCount, float radius)
        {
            Transform[] patrolPoints = new Transform[wayPointCount];

            for (int i = 0; i < wayPointCount; i++)
            {
                float angle = i * Mathf.PI * 2f / wayPointCount;
                Vector3 pointPosition = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius) +
                                        transform.position;

                GameObject point = new("Waypoint_" + i)
                {
                    transform =
                    {
                        position = pointPosition,
                        parent = EnemySpawner.Instance.wayPointsParent
                    }
                };

                patrolPoints[i] = point.transform;
            }

            return patrolPoints;
        }

        #endregion
    }
}