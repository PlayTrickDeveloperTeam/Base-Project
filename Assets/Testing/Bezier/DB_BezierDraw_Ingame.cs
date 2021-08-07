using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herkdess.Tools.DB
{
    [System.Serializable]
    public class DB_BezierDraw_Ingame
    {

        MonoBehaviour Parent;
        Transform Target;
        [SerializeField] BD_BezierData[] Beziers;
        [SerializeField] Material LineMaterial;
        [HideInInspector] public Vector3[] BezierPoints;
        [HideInInspector] public Transform[] BezierTransforms;

        Transform Holder;

        int PointAmount;
        float Distance;
        float DistanceBetweenPoints;

        public void DrawBezier(MonoBehaviour parent, Transform target, int bezierPoints, Transform holder)
        {
            this.Parent = parent;
            this.Target = target;
            this.PointAmount = bezierPoints;
            int totalAmountOfPoints = Beziers.Length * PointAmount;
            BezierPoints = new Vector3[totalAmountOfPoints];
            BezierTransforms = new Transform[totalAmountOfPoints];
            this.Holder = holder;
            PrepareBezier();
        }

        void PrepareBezier()
        {
            Distance = Vector3.Distance(Parent.transform.position, Target.position);
            DistanceBetweenPoints = Distance / BezierPoints.Length;
            for (int i = 0; i < BezierPoints.Length; i++)
            {
                Vector3 _temp = Vector3.zero;
                if (i == 0)
                {
                    _temp = Vector3.zero;
                }
                else
                {
                    _temp = Vector3.MoveTowards(BezierPoints[i - 1], Target.position, DistanceBetweenPoints);
                }

                BezierPoints[i] = _temp;
            }

            for (int x = 0; x < Beziers.Length; x++)
            {
                int firstPoint = x * PointAmount;
                int lastPoint = (x * PointAmount) + PointAmount;
                Beziers[x].DrawBezier(BezierPoints[firstPoint], BezierPoints[lastPoint - 1], PointAmount);
            }

            for (int x = 1; x < Beziers.Length + 1; x++)
            {
                for (int y = 1; y < (BezierPoints.Length / Beziers.Length) + 1; y++)
                {
                    int I1 = ((x - 1) * PointAmount) + y - 1;
                    BezierPoints[I1] = Beziers[x - 1].Points[y - 1];
                }
            }

            for (int i = 0; i < BezierPoints.Length; i++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.localScale = new Vector3(.1f, .1f, .1f);
                obj.transform.position = BezierPoints[i];
                obj.transform.parent = Holder;
                obj.GetComponent<MeshRenderer>().material = LineMaterial;
                BezierTransforms[i] = obj.transform;
            }
        }

        public Vector3[] GetPositions()
        {
            Vector3[] pos = new Vector3[BezierTransforms.Length];
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = BezierTransforms[i].position;
            }
            return pos;
        }

    }
}



