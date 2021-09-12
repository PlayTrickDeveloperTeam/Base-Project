using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace Herkdess.Tools.BehaviorTree
{
    public class GoToCoverNode : Node
    {
        NavMeshAgent agent;
        T_EnemyAI ai;

        public GoToCoverNode(NavMeshAgent agent, T_EnemyAI ai)
        {
            this.agent = agent;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            Transform cover = ai.GetBestCover();
            if (cover == null) return NodeState.Fail;
            ai.SetColor(Color.yellow);
            float distance = Vector3.Distance(cover.position, agent.transform.position);
            if (distance > 2)
            {
                agent.isStopped = false;
                agent.SetDestination(cover.position);
                return NodeState.Running;
            }
            else
            {
                agent.isStopped = true;
                return NodeState.Success;
            }
        }
    }
}