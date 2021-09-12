using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public class RangeNode : Node
    {
        private float range;
        private Transform target;
        private Transform origin;

        public RangeNode(float range, Transform target, Transform origin)
        {
            this.range = range;
            this.target = target;
            this.origin = origin;
        }

        public override NodeState Evaluate()
        {
            float distance = Vector3.Distance(origin.position, target.position);
            return distance <= range ? NodeState.Success : NodeState.Fail;
        }
    }
}