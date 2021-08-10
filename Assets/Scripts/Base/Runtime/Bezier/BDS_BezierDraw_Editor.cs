using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herkdess.Tools
{
    [System.Serializable]
    public class BDS_BezierDraw_Editor
    {
        #region Editor Functions

        MonoBehaviour Parent;

        Transform Target;
        Transform TransformStorage;

        [SerializeField] BDS_BezierData[] Beziers;
        [HideInInspector] public Vector3[] BezierPoints;
        [HideInInspector] public Transform[] BezierTransforms;


        [SerializeField] GameObject ObjectToSpawnPrefab;
        [SerializeField] Material ObjectToSpawnMaterial;
        [SerializeField] Vector3 SpawnedObjectSize = new Vector3(.1f, .1f, .1f);

        [SerializeField] int PerBezierPointCount = 50;

        float DistanceBetweenBeziers;
        float DistanceBetweenPoints;

        public void ResetDebugBezier()
        {
            CanDraw = false;
            if (!DebugBezierControl) return;
            this.DebugBezierControl = false;
            BezierPoints = new Vector3[0];
            GameObject[] activeObjects = new GameObject[TransformStorage.childCount];
            for (int i = 0; i < activeObjects.Length; i++)
            {
                activeObjects[i] = TransformStorage.GetChild(i).gameObject;
            }
            for (int i = 0; i < activeObjects.Length; i++)
            {
                GameObject.DestroyImmediate(activeObjects[i]);
            }
            BezierTransforms = new Transform[0];
        }

        public Vector3[] GetPositions()
        {
            if (BezierTransforms == null) return null;
            Vector3[] pos = new Vector3[BezierTransforms.Length];
            for (int i = 0; i < pos.Length; i++)
            {
                pos[i] = BezierTransforms[i].position;
            }
            return pos;
        }

#if UNITY_EDITOR

        bool DebugBezierControl = false;
        public bool CanDraw = false;
        public void DebugDrawBezier()
        {
            if (!DebugBezierControl) return;
            if (GetPositions().Length < 2) return;
            Gizmos.color = Color.green;
            for (int i = 0; i < GetPositions().Length; i++)
            {
                if (i == GetPositions().Length - 1) break;
                Gizmos.DrawLine(GetPositions()[i], GetPositions()[i + 1]);
            }
        }

        public void DrawBezier(MonoBehaviour parent, Transform target, Transform holder)
        {
            if (PerBezierPointCount <= 0) return;
            CanDraw = true;
            if (DebugBezierControl) return;
            this.DebugBezierControl = true;
            this.Parent = parent;
            this.Target = target;
            int totalAmountOfPoints = Beziers.Length * PerBezierPointCount;
            BezierPoints = new Vector3[totalAmountOfPoints];
            BezierTransforms = new Transform[totalAmountOfPoints];
            this.TransformStorage = holder;
            PrepareBezier();
        }

        public void PrepareBezier()
        {
            DistanceBetweenBeziers = Vector3.Distance(Parent.transform.position, Target.position);
            DistanceBetweenPoints = DistanceBetweenBeziers / BezierPoints.Length;
            for (int i = 0; i < BezierPoints.Length; i++)
            {
                Vector3 _pointToAdd = Vector3.zero;

                if (i == 0)
                    _pointToAdd = Vector3.zero;
                else
                    _pointToAdd = Vector3.MoveTowards(BezierPoints[i - 1], Target.position, DistanceBetweenPoints);
                BezierPoints[i] = _pointToAdd;
            }

            for (int x = 0; x < Beziers.Length; x++)
            {
                int firstPoint = x * PerBezierPointCount;
                int lastPoint = (x * PerBezierPointCount) + PerBezierPointCount - x;
                Beziers[x].DrawBezier(BezierPoints[firstPoint], BezierPoints[lastPoint - 1], PerBezierPointCount);
            }

            for (int x = 1; x < Beziers.Length + 1; x++)
            {
                for (int y = 1; y < (BezierPoints.Length / Beziers.Length) + 1; y++)
                {
                    int I1 = ((x - 1) * PerBezierPointCount) + y - 1;
                    BezierPoints[I1] = Beziers[x - 1].Points[y - 1];
                }
            }

            for (int i = 0; i < BezierPoints.Length; i++)
            {
                if (ObjectToSpawnPrefab == null)
                {
                    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    obj.transform.localScale = SpawnedObjectSize;
                    obj.transform.position = BezierPoints[i];
                    obj.transform.parent = TransformStorage;
                    if (this.ObjectToSpawnMaterial != null)
                        obj.GetComponent<MeshRenderer>().material = ObjectToSpawnMaterial;
                    BezierTransforms[i] = obj.transform;
                }
                else
                {
                    GameObject obj = GameObject.Instantiate(ObjectToSpawnPrefab);
                    obj.transform.localPosition = SpawnedObjectSize;
                    obj.transform.position = BezierPoints[i];
                    obj.transform.parent = TransformStorage;
                    //if (i + 1 <= BezierPoints.Length)
                    //obj.transform.LookAt(BezierPoints[i + 1]);
                    BezierTransforms[i] = obj.transform;
                }

            }
        }

#endif
        #endregion
    }
}

