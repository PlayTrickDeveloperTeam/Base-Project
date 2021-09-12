using System.Collections;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public class IsCoveredNode : Node
    {
        private Transform target;
        private Transform origin;

        public IsCoveredNode(Transform target, Transform origin)
        {
            this.target = target;
            this.origin = origin;
        }

        public override NodeState Evaluate()
        {
            RaycastHit hit;
            if (Physics.Raycast(origin.position, target.position - origin.position, out hit))
            {
                if (hit.collider.transform != target) return NodeState.Success;
            }
            return NodeState.Fail;
        }

    }
}