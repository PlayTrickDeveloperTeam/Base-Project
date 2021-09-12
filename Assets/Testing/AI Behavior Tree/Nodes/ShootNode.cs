using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Herkdess.Tools.BehaviorTree
{
    public class ShootNode : Node
    {
        NavMeshAgent agent;
        private T_EnemyAI ai;

        public ShootNode(NavMeshAgent agent, T_EnemyAI ai)
        {
            this.agent = agent;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            agent.isStopped = true;
            ai.SetColor(Color.green);
            return NodeState.Running;
        }
    }
}