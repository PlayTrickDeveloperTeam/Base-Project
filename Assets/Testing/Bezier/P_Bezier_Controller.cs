using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Herkdess.Tools.DB;
#if UNITY_EDITOR
using Sirenix.OdinInspector;

#endif
//[ExecuteAlways]
public class P_Bezier_Controller : MonoBehaviour
{

    public Transform Target;
    public Transform Cube;
    [Range(5, 50)]
    public float SpeedStep;

    public Transform Aimer;
    public DB_BezierDraw_Editor BezierDrawer;

#if UNITY_EDITOR
    [Button]
    public void StarDrawing()
    {
        BezierDrawer.DrawBezier(this, Target, 20, Aimer);
    }

    [Button]
    public void ResetDrawing()
    {
        BezierDrawer.ResetDebugBezier();
    }

    private void OnDrawGizmos()
    {
        //BezierDrawer.DebugDrawBezier();
    }

#endif


    void Start()
    {
        //BezierDrawer.BezierDrawerRead(this, Target, 20, Aimer);
        //StartCoroutine(MoveCube());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(MoveCube());
        }
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






