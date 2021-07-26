using System.Collections;
using UnityEngine;

namespace Main
{
    public class M_LevelPrep : MonoBehaviour
    {
        private int levelCount;

        private void Awake()
        {
            OnLevelAwake();
        }
        private void OnEnable()
        {
            OnLevelOnEnable();
        }

        private void Start()
        {
            OnLevelStart();
        }

        public void OnLevelReady()
        {
            OnLevelInitate();
        }

        public void OnLevelAwake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelAwake();
            }
        }

        public void OnLevelOnEnable()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelOnEnable();
            }
        }

        public void OnLevelStart()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelStart();
            }

        }

        public void OnLevelInitate()
        {
            M_GameManager.instance.MainSaveData.SetData(SE_DataTypes.PlayerLevel, levelCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelInitate();
            }
        }

        public void OnLevelCommand()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelCommand();
            }
        }
        private void Update()
        {
            if (M_GameManager.instance.CurrentGameState != GameStates.Playing) return;
            if (Input.GetMouseButtonDown(0))
            {
                M_MenuManager_1.instance.ActivateEndGame(.5f, true);
            }
        }
    }
}