using System.Collections.Generic;

namespace _GAME_.Scripts.Core.BehaviorTree
{
    public class Selector : Node
    {
        public Selector() : base()
        {
        }
        
        public Selector(List<Node> children) : base(children){}

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.FAILURE:
                        continue;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }
    }
}