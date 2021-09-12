using System;
using System.Collections;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public class IsCoverAvaliableNode : Node
    {
        Cover[] avaliableCovers;
        Transform target;
        T_EnemyAI ai;
        LayerMask coverMask;
        public IsCoverAvaliableNode(Cover[] avaliableCovers, Transform target, T_EnemyAI ai, LayerMask coverMask)
        {
            this.avaliableCovers = avaliableCovers;
            this.target = target;
            this.ai = ai;
            this.coverMask = coverMask;
        }

        public override NodeState Evaluate()
        {
            Transform bestSpot = FindBestSpot();
            ai.SetBestCover(bestSpot);
            return bestSpot != null ? NodeState.Success : NodeState.Fail;
        }

        private Transform FindBestSpot()
        {
            float minAngle = 90;
            Transform bestSpot = null;
            for (int i = 0; i < avaliableCovers.Length; i++)
            {
                Transform bestSpotInCover = FindBestSpotInCover(avaliableCovers[i], ref minAngle);
                if (bestSpotInCover != null)
                {
                    bestSpot = bestSpotInCover;
                }
            }
            return bestSpot;
        }

        private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
        {
            Transform[] avaliableSpots = cover.GetCoverPositions();
            Transform bestSpot = null;

            for (int i = 0; i < avaliableSpots.Length; i++)
            {
                Vector3 direction = target.position - avaliableSpots[i].position;
                if (CheckIfSpotIsValid(avaliableSpots[i]))
                {
                    float angle = Vector3.Angle(avaliableSpots[i].forward, direction);
                    if (angle < minAngle)
                    {
                        minAngle = angle;
                        bestSpot = avaliableSpots[i];
                    }
                }
            }
            return bestSpot;
        }

        private bool CheckIfSpotIsValid(Transform spot)
        {
            RaycastHit hit;
            Vector3 direction = target.position - spot.position;
            if (Physics.Raycast(spot.position, direction, out hit, coverMask))
            {
                if (hit.collider.transform != target)
                {
                    return true;
                }
            }
            return false;
        }
    }
}