using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Herkdess.Tools.BehaviorTree
{
    public class ChaseNode : Node
    {
        Transform target;
        NavMeshAgent agent;
        T_EnemyAI ai;

        public ChaseNode(Transform target, NavMeshAgent agent, T_EnemyAI ai)
        {
            this.target = target;
            this.agent = agent;
            this.ai = ai;
        }

        public override NodeState Evaluate()
        {
            ai.SetColor(Color.yellow);
            float distance = Vector3.Distance(target.position, agent.transform.position);
            if (distance > 2)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
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