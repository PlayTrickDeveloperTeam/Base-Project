using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public class Inventor : Node
    {
        protected Node node;

        public Inventor(Node node)
        {
            this.node = node;
        }
        public override NodeState Evaluate()
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    break;
                case NodeState.Success:
                    _nodeState = NodeState.Fail;
                    break;
                case NodeState.Fail:
                    _nodeState = NodeState.Success;
                    break;
                default:
                    break;
            }
            _nodeState = NodeState.Fail;
            return _nodeState;

        }
    }
}