using UnityEngine;

namespace _GAME_.Scripts.Core.BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            _root?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}