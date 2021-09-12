using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public class HealthNode : Node
    {
        private T_EnemyAI ai;
        private float threshold;

        public HealthNode(T_EnemyAI ai, float threshold)
        {
            this.ai = ai;
            this.threshold = threshold;
        }

        public override NodeState Evaluate()
        {
            return ai.currentHealth <= threshold ? NodeState.Success : NodeState.Fail;
        }
    }
}