using System.Collections;
using UnityEngine;

namespace Base
{
    public class Example_Spawner : MonoBehaviour, B_LC_ISpawnedLevelObject
    {
        Vector3 cubeSpawnPos = Vector3.zero;

        public void OnLevelAwake()
        {

        }
        public void OnLevelOnEnable()
        {

        }
        public void OnLevelStart()
        {

        }

        public void OnLevelInitate()
        {
            for (int i = 0; i < B_VFM_EffectsManager.instance.GetObjectPool("ExampleTakTak").PrewarmCount; i++)
            {
                float x = (i - 1.5f) * 2;
                Vector3 _spawnPos = new Vector3(0, x, 0);
                B_VFM_EffectsManager.instance.SpawnObjFromPool("ExampleTakTak", _spawnPos);
            }
        }

        public void OnLevelCommand()
        {

        }
    }
}