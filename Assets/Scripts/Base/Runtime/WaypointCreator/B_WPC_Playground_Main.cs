﻿using System.Collections;
using UnityEngine;

#if UNITY_EDITOR

#endif

namespace Base
{
    public class B_WPC_Playground_Main : MonoBehaviour
    {
        [SerializeField] B_WPC_WaypointCreator WaypointCreator;

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
        public B_WPC_Waypoint GetWaypointTransform()
        {
            B_WPC_Waypoint _temp = WaypointCreator.GetAVaibleWaypoint();
            if (_temp == null) { BMM_MenuManager_Project.instance.ActivateEndGame(.5f, true); return null; }
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
