using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herkdess.Tools
{
    [System.Serializable]
    public class BDS_BezierData
    {
        Vector3 StartPoint;
        Vector3 EndPoint;
        [SerializeField] Transform ControlPoint;
        [HideInInspector] public Vector3[] Points;

        public void DrawBezier(Vector3 startPoint, Vector3 endPoint, int count)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            Points = new Vector3[count];
            DrawBezier(count);
        }

        void DrawBezier(int count)
        {
            for (int i = 1; i < count + 1; i++)
            {
                float t = i / (float)count;
                Points[i - 1] = CalculateQuadricBezierPoints(t, StartPoint, ControlPoint.position, EndPoint);
            }
        }

        public Vector3 CalculateQuadricBezierPoints(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }
    }

}

