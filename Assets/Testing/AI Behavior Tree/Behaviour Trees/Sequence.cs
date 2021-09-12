using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
namespace Herkdess.Tools.BehaviorTree
{
    public class Sequence : Node
    {
        protected List<Node> nodes = new List<Node>();

        public Sequence(List<Node> nodes)
        {
            this.nodes = nodes;
        }
        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;
            foreach (var node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Running:
                        isAnyChildRunning = true;
                        break;
                    case NodeState.Success:

                        break;
                    case NodeState.Fail:
                        _nodeState = NodeState.Fail;
                        return _nodeState;
                    default:

                        break;
                }
            }

            _nodeState = isAnyChildRunning ? NodeState.Running : NodeState.Success;
            return _nodeState;

        }
    }

}