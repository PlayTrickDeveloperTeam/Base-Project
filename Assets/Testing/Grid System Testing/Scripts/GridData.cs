using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Sirenix.OdinInspector;
using Base;
using System.Linq;
using Unity.Mathematics;

namespace Herkdess.Tools.Grid
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Grid Data", menuName = "Grid System/Create Grid Data")]
    public class GridData : ScriptableObject
    {
        [OnValueChanged("RecalculateGrid")]
        public int2 GridSize;
        [OnValueChanged("RecalculateGrid")]
        public float CellSize;
        [OnValueChanged("RecalculateGrid")]
        public Material CellMaterial;
        [OnValueChanged("RecalculateGrid")]
        public GameObject DefaultObject;
        //[OnValueChanged("RecalculateGrid")]
        //public bool SpawnAcordingToTheCamera;


        [HideInInspector] public GridCell[,] Cells;

        Transform SpawnPosition;
        List<GameObject> SpawnedCells;

        public void RecalculateGrid()
        {
            DrawGrid(this.SpawnPosition, this.DefaultObject);
            this.Save();
            //if (this != this.LoadScriptableObject(SavePath))
            //{
            //    ////GridSize = new int2(2, 2);
            //    ////CellSize = 5f;
            //    //////CellMaterial = new Material(Shader);
            //    ////this.Cells = new GridCell[2, 2];
            //    ////GameObject SpawnPosition = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //    //DrawGrid(SpawnPosition, GameObject.CreatePrimitive(PrimitiveType.Quad));
            //    //this.Save();
            //}
            //else
            //{
            //    DrawGrid(this.SpawnPosition, this.DefaultObject);
            //    this.Save();
            //}
        }

        public void DrawGrid(Transform SpawnPosition, GameObject Obj)
        {
            this.SpawnPosition = SpawnPosition;
            this.DefaultObject = Obj;
            if (this.SpawnPosition.childCount > 0)
                ResetGrid();


            if (GridSize.x < 0 || GridSize.y < 0) return;

            this.Cells = new GridCell[GridSize.x, GridSize.y];
            SpawnedCells = new List<GameObject>();

            //Vector3 spawnPos = SpawnAcordingToTheCamera ? Camera.main.WorldPositionsOfCameraWithY(0)[0] : this.SpawnPosition.position;
            Vector3 spawnPos = this.SpawnPosition.position;
            for (int x = 0; x < Cells.GetLength(0); x++)
                for (int z = 0; z < Cells.GetLength(1); z++)
                {
                    GameObject obj = GameObject.Instantiate(Obj);
                    obj.GetComponent<MeshRenderer>().material = CellMaterial;
                    obj.GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
                    obj.transform.parent = this.SpawnPosition;
                    obj.transform.position = new Vector3(x, 0, z) * CellSize + spawnPos;
                    obj.transform.localRotation = Quaternion.identity;
                    obj.transform.ResizeObject(CellSize);
                    CellFunctions function = obj.GetComponent<CellFunctions>();
                    if (function == null)
                    {
                        obj.AddComponent<CellFunctions>().Data = this;
                        function = obj.GetComponent<CellFunctions>();
                    }
                    else
                    {
                        obj.GetComponent<CellFunctions>().Data = this;
                    }
                    SpawnedCells.Add(obj);

                    Cells[x, z] = new GridCell(new Vector3(x, 0, z) * CellSize + spawnPos, new int2(x, z), this, function);
                }
            for (int x = 0; x < Cells.GetLength(0); x++)
                for (int z = 0; z < Cells.GetLength(1); z++)
                {
                    Cells[x, z].AfterSpawnDone();
                }
        }

        public GridCell GetGridCell(int x, int y)
        {
            //if (x < 0 || x > Cells.GetLength(0) || y < 0 || y > Cells.GetLength(1))
            //{
            //    Debug.Log("Returning Null");
            //    return null;
            //}

            return Cells[x, y];

        }


        string SavePath = "/GridData.save";
        [Button]
        public void Save()
        {
            this.SaveScriptableObject(SavePath);
        }

        [Button]
        public void Load()
        {
            this.LoadScriptableObject(SavePath, false);
            DrawGrid(this.SpawnPosition, this.DefaultObject);
        }

        [Button]
        public void ResetGrid()
        {
            SpawnedCells.ForEach(t => DestroyImmediate(t));
        }
    }
}