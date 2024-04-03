using OrangeBear.EventSystem;

namespace _GAME_.Scripts.Core.BehaviorTree
{
    public abstract class Tree : Bear
    {
        private Node _root;

        protected virtual void Start()
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