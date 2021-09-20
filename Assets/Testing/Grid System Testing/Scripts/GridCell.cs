using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using Unity.Mathematics;

namespace Herkdess.Tools.Grid
{
    public class GridCell
    {
        GridData Data;
        public Vector3 Position;
        public int2 GridPosition;
        public bool InUse;
        public GridCell[] Neighbors;
        public CellFunctions SpawnedFunction;
        public GridCell(Vector3 pos, int2 GridNumber, GridData data, CellFunctions spawnedFunction)
        {
            this.Data = data;
            this.Position = pos;
            this.GridPosition = GridNumber;
            this.Neighbors = new GridCell[8];
            this.SpawnedFunction = spawnedFunction;

        }

        public void AfterSpawnDone()
        {
            SpawnedFunction.Ready(this);
        }
    }


    public class CellData : ScriptableObject
    {
        //GridCell[,] CellsArray;
        //List<GameObject> CellObjectsInArray;
        //public CellData(int2 CellArraySize, Vector3[,] positions, GameObject GridArrayObject)
        //{
        //    this.CellsArray = new GridCell[CellArraySize.x, CellArraySize.y];
        //    this.CellObjectsInArray = new List<GameObject>();
        //    for (int x = 0; x < CellsArray.GetLength(0); x++)
        //        for (int z = 0; z < CellsArray.GetLength(1); z++)
        //        {
        //            CellsArray[x, z] = new GridCell(positions[x, z]);
        //            //Cells[x, y] = new GridCell(new Vector3(x, 0, y) * CellSize + this.SpawnPosition.position);
        //            GameObject obj = GameObject.Instantiate(GridArrayObject);
        //            obj.GetComponent<MeshRenderer>().material = CellMaterial;
        //            obj.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
        //            obj.transform.parent = this.SpawnPosition;
        //            obj.transform.position = CellsArray[x, z].Position;
        //            obj.transform.localRotation = Quaternion.identity;
        //            obj.transform.ResizeObject(CellSize);
        //            SpawnedCells.Add(obj);
        //        }
        //}

    }
}
