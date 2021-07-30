using System.Collections;
using UnityEngine;

namespace Base
{
    public class Playground_Main : MonoBehaviour
    {
        [SerializeField] WaypointCreator WaypointCreator;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            WaypointCreator.DrawDebugLine();
        }
#endif

        public void AddWaypoint(Transform target)
        {
            WaypointCreator.AddNewWaypoint(target);
        }
        public Waypoint GetWaypointTransform()
        {
            Waypoint _temp = WaypointCreator.GetAVaibleWaypoint();
            if (_temp == null) { M_MenuManager_1.instance.ActivateEndGame(.5f, true); return null; }
            return WaypointCreator.GetAVaibleWaypoint();
        }
        private void Start()
        {
            for (int i = 0; i < WaypointCreator.WaypointHolders.Count; i++)
            {
                WaypointCreator.WaypointHolders[i].gameObject.SetActive(false);
            }
        }

        public void OnLevelAwake()
        {

        }

        public void OnLevelOnEnable()
        {

        }

        public void OnLevelStart()
        {

        }

        public void OnLevelInitate()
        {

        }

        public void OnLevelCommand()
        {

        }
    }
}
