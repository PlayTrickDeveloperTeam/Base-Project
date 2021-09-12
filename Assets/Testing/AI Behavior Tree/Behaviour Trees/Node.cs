using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public enum NodeState { Running, Success, Fail }
    [System.Serializable]
    public abstract class Node
    {
        protected NodeState _nodeState;
        public NodeState nodestate { get { return _nodeState; } }
        public abstract NodeState Evaluate();
    }
}

