using System.Collections;
using UnityEngine;

namespace Herkdess.Tools.BehaviorTree
{
    public class Cover : MonoBehaviour
    {
        [SerializeField] Transform[] coverSpots;

        private void Awake()
        {
            coverSpots = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                coverSpots[i] = transform.GetChild(i);
            }
        }

        public Transform[] GetCoverPositions()
        {
            return coverSpots;
        }

    }
}