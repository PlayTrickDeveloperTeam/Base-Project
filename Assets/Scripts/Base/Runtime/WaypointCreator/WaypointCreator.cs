using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
#endif

namespace Base
{
    [System.Serializable]
    public class WaypointCreator
    {
        private GameObject Parent;
        [HideInInspector] public List<Transform> WaypointHolders = new List<Transform>();
        public List<Waypoint> Waypoints = new List<Waypoint>();
        public void AddNewWaypoint(Transform target)
        {
            Waypoint newWaypoint = new Waypoint(target);
            Waypoints.Add(newWaypoint);
        }

        public Waypoint GetAVaibleWaypoint()
        {
            for (int i = 0; i < Waypoints.Count; i++)
            {
                if (Waypoints[i].Reached) continue;
                return Waypoints[i];
            }
            return null;
        }

#if UNITY_EDITOR
        [Button("Create New Waypoint")]
        public void CreateNewWaypoint()
        {
            Parent = Selection.activeGameObject;
            GameObject NewWaypoint = CreateNewHolder();
            if (WaypointHolders.Count <= 0) NewWaypoint.transform.position = Vector3.zero;
            else { NewWaypoint.transform.position = WaypointHolders[WaypointHolders.Count - 1].position; }
            Waypoint WaypointToAdd = new Waypoint(NewWaypoint.transform);
            Selection.activeGameObject = NewWaypoint;
            Waypoints.Add(WaypointToAdd);
            WaypointHolders.Add(NewWaypoint.transform);
        }

        [Button("Delete Current Waypoints")]
        public void DeleteWaypoints()
        {
            GameObject[] waypointObjects = new GameObject[WaypointHolders.Count];
            for (int i = 0; i < Waypoints.Count; i++)
            {
                waypointObjects[i] = WaypointHolders[i].gameObject;
            }

            foreach (var item in waypointObjects)
            {
                GameObject.DestroyImmediate(item);
            }

            Waypoints = new List<Waypoint>();
            WaypointHolders = new List<Transform>();
        }

        public void DrawDebugLine()
        {
            if (Waypoints.Count < 2) return;
            for (int i = 0; i < Waypoints.Count; i++)
            {
                if (i == Waypoints.Count - 1) break;
                Gizmos.DrawLine(Waypoints[i].W_Transform.position, Waypoints[i + 1].W_Transform.position);
            }
            Gizmos.color = Color.green;
        }

        GameObject CreateNewHolder()
        {
            GameObject newWaypointHolder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            newWaypointHolder.GetComponent<MeshRenderer>().material = (Material)AssetDatabase.LoadAssetAtPath("Assets/Visuals/General Materials/WaypointCreator/Mat_Waypoint.mat", typeof(Material));
            newWaypointHolder.name = "Waypoint " + Waypoints.Count;
            newWaypointHolder.transform.parent = Parent.transform.GetChild(0).transform;
            Component.DestroyImmediate(newWaypointHolder.GetComponent<Collider>());
            return newWaypointHolder;
        }
#endif
    }
    [System.Serializable]
    public class Waypoint
    {
        public Transform W_Transform;
        public bool Reached = false;

        public Waypoint(Transform transform)
        {
            this.W_Transform = transform;
            Reached = false;
        }
    }
}

