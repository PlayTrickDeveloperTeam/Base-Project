using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Herkdess.Tools;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

namespace Herkdess.Tools
{
    [ExecuteAlways]
    public class BDS_Bezier_Controller : MonoBehaviour
    {

        public Transform Target;
        public Transform Cube;
        [Range(5, 50)]
        public float SpeedStep;

        public Transform Aimer;
        public BDS_BezierDraw_Editor BezierDrawer;

#if UNITY_EDITOR
        [Button]
        public void StarDrawing()
        {
            BezierDrawer.DrawBezier(this, Target, Aimer);
        }

        [Button]
        public void ResetDrawing()
        {
            BezierDrawer.ResetDebugBezier();
        }

#endif

        private void Update()
        {
#if UNITY_EDITOR
            EditorFunctions();
#endif
            if (Input.GetMouseButtonDown(1))
            {
                StartCoroutine(MoveCube());
            }
        }

        void EditorFunctions()
        {
            if (!BezierDrawer.CanDraw) return;
            BezierDrawer.ResetDebugBezier();
            BezierDrawer.DrawBezier(this, Target, Aimer);
            //BezierDrawer.DebugDrawBezier();
        }


        IEnumerator MoveCube()
        {
            for (int i = 0; i < BezierDrawer.GetPositions().Length; i++)
            {
                while (Cube.transform.position != BezierDrawer.GetPositions()[i])
                {
                    Cube.transform.position = Vector3.MoveTowards(Cube.transform.position, BezierDrawer.GetPositions()[i], Time.deltaTime * SpeedStep);
                    yield return new WaitForEndOfFrame();
                }
            }
        }

    }
}







