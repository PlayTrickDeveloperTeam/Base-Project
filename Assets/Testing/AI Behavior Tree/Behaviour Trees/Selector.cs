using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public class Selector : Node
    {
        protected List<Node> nodes = new List<Node>();

        public Selector(List<Node> nodes)
        {
            this.nodes = nodes;
        }
        public override NodeState Evaluate()
        {
            foreach (var node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Running:
                        _nodeState = NodeState.Running;
                        return _nodeState;
                    case NodeState.Success:
                        _nodeState = NodeState.Success;
                        return _nodeState;
                    case NodeState.Fail:
                        break;
                    default:

                        break;
                }
            }

            _nodeState = NodeState.Fail;
            return _nodeState;

        }
    }

}