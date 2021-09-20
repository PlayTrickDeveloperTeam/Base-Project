using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using Base;
namespace Herkdess.Tools.Grid
{
    public enum TesterState { Building, Ready }
    public class GridTester : MonoBehaviour
    {
        public static GridTester instance;
        B_IPC_InputController IPC;
        public LayerMask GridMask;
        TesterState state;

        GameObject buildingPrefab;
        Building_Base SelectedBuilding;

        private void Awake()
        {

        }

        private void Start()
        {
            IPC = new B_IPC_InputController(this, .5f, 2f, true);
            IPC.OnDragAction += MouseControl;
            IPC.ActivateInputController();
            state = TesterState.Ready;
        }

        private void Update()
        {
            //IPC.Run();
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Transform taken = Input.mousePosition.GetWorldObject(Camera.main, GridMask);
                if (taken == null) return;
                CellFunctions cell = taken.GetComponent<CellFunctions>();
                if (cell != null)
                    cell.ChangeColor();
            }
        }

        void MouseControl()
        {
            //CellFunctions cell = IPC.MouseCurrentPosition.GetWorldObject(Camera.main, GridMask).GetComponent<CellFunctions>();
            //if (cell != null)
            //    cell.ChangeColor();
        }

        void SelectBuilding(GameObject obj)
        {
            buildingPrefab = obj;
            GameObject SpawnedBuilding = Instantiate(buildingPrefab);
            SelectedBuilding = SpawnedBuilding.GetComponent<Building_Base>();
        }


        #region Grid
        public GridData Data;
        public Transform SpawnPosition;
        public GameObject ObjToSpawn;
        #endregion

        [Button]
        public void SetGrid()
        {
            Data.DrawGrid(SpawnPosition, ObjToSpawn);
        }

        [Button]
        public void ResetGrid()
        {
            Data.ResetGrid();
        }






    }
}