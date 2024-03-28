using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.Enemy.Nodes;
using UnityEngine;
using Tree = _GAME_.Scripts.Core.BehaviorTree.Tree;

namespace _GAME_.Scripts.Enemy
{
    public class EnemyBehaviorTree : Tree
    {
        #region Public Variables

        public Transform[] waypoints;
        public static float speed = 2;

        #endregion
        protected override Node SetupTree()
        {
            Node root = new PatrolNode(transform, waypoints);

            return root;
        }
    }
}