using System.Collections;
using UnityEngine;

namespace Base
{
    public class B_LC_LevelPreparator : MonoBehaviour
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
                B_LC_ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<B_LC_ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelAwake();
            }
        }

        public void OnLevelOnEnable()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                B_LC_ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<B_LC_ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelOnEnable();
            }
        }

        public void OnLevelStart()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                B_LC_ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<B_LC_ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelStart();
            }

        }

        public void OnLevelInitate()
        {
            B_GM_GameManager.instance.MainSaveData.SetData(B_SE_DataTypes.PlayerLevel, levelCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                B_LC_ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<B_LC_ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelInitate();
            }
        }

        public void OnLevelCommand()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                B_LC_ISpawnedLevelObject InterfaceObject = transform.GetChild(i).GetComponent<B_LC_ISpawnedLevelObject>();
                if (InterfaceObject == null) continue;
                InterfaceObject.OnLevelCommand();
            }
        }

    }
}