using _GAME_.Scripts.Core.BehaviorTree;
using _GAME_.Scripts.GlobalVariables;
using _GAME_.Scripts.Managers;
using UnityEngine;
using UnityEngine.AI;

namespace _GAME_.Scripts.Enemy.Nodes
{
    public class SlipNode : Node
    {
        #region Private Variables

        private NavMeshAgent _navMeshAgent;
        private EnemyBehaviorTree _enemyBehaviorTree;
        private float _speed;
        private float _stunTime;
        private float _waitCounter;

        #endregion

        #region Constructor

        public SlipNode(EnemyBehaviorTree enemyBehaviorTree, NavMeshAgent navMeshAgent)
        {
            _enemyBehaviorTree = enemyBehaviorTree;
            _navMeshAgent = navMeshAgent;
            _speed = _navMeshAgent.speed;
            _stunTime = DataManager.Instance.GetStunSkillData().stunTime;
        }

        #endregion

        #region Inherit Methods

        public override NodeState Evaluate()
        {
            if (_enemyBehaviorTree.isSlipping)
            {
                _navMeshAgent.speed = 0;
                state = NodeState.RUNNING;

                if (_enemyBehaviorTree.enemyType == EnemyType.Robot) return state;

                if (_waitCounter <= _stunTime)
                {
                    _waitCounter += Time.deltaTime;
                }

                else
                {
                    _enemyBehaviorTree.isSlipping = false;
                }
            }
            else
            {
                _navMeshAgent.speed = _speed;
                state = NodeState.FAILURE;
            }

            return state;
        }

        #endregion
    }
}