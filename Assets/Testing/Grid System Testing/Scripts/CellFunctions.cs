using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Herkdess.Tools.Grid
{
    public class CellFunctions : MonoBehaviour
    {
        [HideInInspector] public GridData Data;
        [HideInInspector] public GridCell Cell;
        [HideInInspector] public bool InUse;

        public void Ready(GridCell cell)
        {
            this.Cell = cell;
        }

        public void ChangeColor()
        {
            InUse = InUse ? false : true;
            GetComponent<MeshRenderer>().material.color = InUse ? Color.green : Color.red;
        }
    }
}