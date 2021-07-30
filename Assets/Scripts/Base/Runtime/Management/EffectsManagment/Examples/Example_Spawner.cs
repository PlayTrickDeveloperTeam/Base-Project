using System.Collections;
using UnityEngine;

namespace Base
{
    public class Example_Spawner : MonoBehaviour, ISpawnedLevelObject
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
            for (int i = 0; i < VFM_EffectsManager.instance.GetObjectPool("ExampleTakTak").PrewarmCount; i++)
            {
                float x = (i - 1.5f) * 2;
                Vector3 _spawnPos = new Vector3(0, x, 0);
                VFM_EffectsManager.instance.SpawnObjFromPool("ExampleTakTak", _spawnPos);
            }
        }

        public void OnLevelCommand()
        {

        }
    }
}