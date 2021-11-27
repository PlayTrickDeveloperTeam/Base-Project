using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.OdinInspector;

#endif

namespace Base {
    public abstract class B_OPS_Pooler_Base : MonoBehaviour {
        public List<ObjectsToPool> PoolsList;

        //Distance from spawn içerisine bir sayı girilmesi lazım
        private float distanceFromSpawn, spawnOffset;

        private Vector3 firstSpawnPoint = new Vector3(8000, 7000, 9000);
        public Dictionary<string, Queue<GameObject>> PoolsDictionary;

        public void InitiatePooller() {
            PoolsDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (var Pools in PoolsList) {
                var objPool = new Queue<GameObject>();
                Pools.PoolName = Pools.ObjectPrefab.name;
                for (var i = 0; i < Pools.PrewarmCount; i++) {
                    var SpawnOffset = i * distanceFromSpawn + 30;
                    var SpawnPos = new Vector3(SpawnOffset, SpawnOffset, SpawnOffset);
                    var toSpawnObj = Instantiate(Pools.ObjectPrefab, SpawnPos, Quaternion.identity);
                    ObjectSpawnHelper(toSpawnObj);
                    objPool.Enqueue(toSpawnObj);
                }

                PoolsDictionary.Add(Pools.PoolName, objPool);
            }
        }

        public void InitiatePooller(Transform parent) {
            PoolsDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (var Pools in PoolsList) {
                var objPool = new Queue<GameObject>();
                for (var i = 0; i < Pools.PrewarmCount; i++) {
                    var SpawnOffset = i * distanceFromSpawn + 30;
                    var SpawnPos = new Vector3(SpawnOffset, SpawnOffset, SpawnOffset);
                    var toSpawnObj = Instantiate(Pools.ObjectPrefab, parent);
                    toSpawnObj.transform.rotation = Quaternion.identity;
                    ObjectSpawnHelper(toSpawnObj);
                    objPool.Enqueue(toSpawnObj);
                }
                PoolsDictionary.Add(Pools.PoolName, objPool);
            }
        }

        public void AddPool(GameObject objPrefab, string prefabName, int spawnCount) {
            if (PoolsDictionary.ContainsKey(objPrefab.name)) return;
            var newPool = new ObjectsToPool();
            newPool.PoolName = objPrefab.name;
            newPool.ObjectPrefab = objPrefab;
            newPool.PrewarmCount = spawnCount;
            PoolsList.Add(newPool);
            var objPool = new Queue<GameObject>();
            for (var i = 0; i < spawnCount; i++) {
                spawnOffset = i * distanceFromSpawn + 30;
                var SpawnPos = new Vector3(spawnOffset, spawnOffset, spawnOffset);
                var toSpawnObj = Instantiate(newPool.ObjectPrefab, SpawnPos, Quaternion.identity);
                toSpawnObj.GetComponent<ObjectsToPool>().PoolName = newPool.PoolName;
                ObjectSpawnHelper(toSpawnObj);
                objPool.Enqueue(toSpawnObj);
            }
            PoolsDictionary.Add(newPool.PoolName, objPool);
        }
#if UNITY_EDITOR

        protected void AddPoolInEditor(GameObject objPrefab, string prefabName, int spawnCount) {
            if (PoolsList == null) PoolsList = new List<ObjectsToPool>();
            var newPool = new ObjectsToPool();
            newPool.PoolName = objPrefab.name;
            newPool.ObjectPrefab = objPrefab;
            newPool.PrewarmCount = spawnCount;
            PoolsList.Add(newPool);
        }

#endif
        private void ObjectSpawnHelper(GameObject obj) {
            obj.SetActive(true);
            var pooledObj = obj.GetComponent<B_OPS_IPooledObject>();
            if (pooledObj != null) pooledObj.OnFirstSpawn();

            obj.SetActive(false);
        }

        public GameObject SpawnObjFromPool(string objectPoolName, Vector3 spawnPosition) {
            if (!PoolsDictionary.ContainsKey(objectPoolName)) return null;

            var objectToSpawn = PoolsDictionary[objectPoolName].Dequeue();
            objectToSpawn.transform.position = spawnPosition;
            PoolsDictionary[objectPoolName].Enqueue(objectToSpawn);
            objectToSpawn.SetActive(true);
            var pulledObjectInterface = objectToSpawn.GetComponent<B_OPS_IPooledObject>();
            if (pulledObjectInterface != null) pulledObjectInterface.OnObjectSpawn();
            return objectToSpawn;
        }

        public GameObject SpawnObjFromPool(object obj, Vector3 spawnPosition) {
            //string PoolName = Enum.GetName(typeof(DemoPoolerTester), DemoPoolerTester.Lar);
            if (!PoolsDictionary.ContainsKey(obj.ToString())) return null;

            var objectToSpawn = PoolsDictionary[obj.ToString()].Dequeue();
            objectToSpawn.transform.position = spawnPosition;
            PoolsDictionary[obj.ToString()].Enqueue(objectToSpawn);
            objectToSpawn.SetActive(true);
            var pulledObjectInterface = objectToSpawn.GetComponent<B_OPS_IPooledObject>();
            if (pulledObjectInterface != null) pulledObjectInterface.OnObjectSpawn();
            return objectToSpawn;
        }

        public GameObject SpawnObjFromPool(string objectPoolName, Vector3 spawnPosition, Quaternion spawnRotation) {
            if (!PoolsDictionary.ContainsKey(objectPoolName)) return null;

            var objectToSpawn = PoolsDictionary[objectPoolName].Dequeue();
            objectToSpawn.transform.position = spawnPosition;
            objectToSpawn.transform.rotation = spawnRotation;
            PoolsDictionary[objectPoolName].Enqueue(objectToSpawn);
            objectToSpawn.SetActive(true);
            var pulledObjectInterface = objectToSpawn.GetComponent<B_OPS_IPooledObject>();
            if (pulledObjectInterface != null) pulledObjectInterface.OnObjectSpawn();
            return objectToSpawn;
        }
        public GameObject SpawnObjFromPool(object objectPoolName, Vector3 spawnPosition, Quaternion spawnRotation) {
            if (!PoolsDictionary.ContainsKey(objectPoolName.ToString())) return null;

            var objectToSpawn = PoolsDictionary[objectPoolName.ToString()].Dequeue();
            objectToSpawn.transform.position = spawnPosition;
            objectToSpawn.transform.rotation = spawnRotation;
            PoolsDictionary[objectPoolName.ToString()].Enqueue(objectToSpawn);
            objectToSpawn.SetActive(true);
            var pulledObjectInterface = objectToSpawn.GetComponent<B_OPS_IPooledObject>();
            if (pulledObjectInterface != null) pulledObjectInterface.OnObjectSpawn();
            return objectToSpawn;
        }


        public GameObject SpawnObjFromPool(string objectPoolName, Vector3 spawnPosition, Vector3 spawnRotation, Transform spawnParent) {
            if (!PoolsDictionary.ContainsKey(objectPoolName)) return null;

            var objectToSpawn = PoolsDictionary[objectPoolName].Dequeue();
            objectToSpawn.transform.position = spawnPosition;
            objectToSpawn.transform.rotation = Quaternion.Euler(spawnRotation);
            objectToSpawn.transform.SetParent(spawnParent);
            PoolsDictionary[objectPoolName].Enqueue(objectToSpawn);
            objectToSpawn.SetActive(true);
            var pulledObjectInterface = objectToSpawn.GetComponent<B_OPS_IPooledObject>();
            if (pulledObjectInterface != null) pulledObjectInterface.OnObjectSpawn();
            return objectToSpawn;
        }

        public GameObject SpawnObjFromPool(object objectPoolName, Vector3 spawnPosition, Vector3 spawnRotation, Transform spawnParent) {
            if (!PoolsDictionary.ContainsKey(objectPoolName.ToString())) return null;

            var objectToSpawn = PoolsDictionary[objectPoolName.ToString()].Dequeue();
            objectToSpawn.transform.position = spawnPosition;
            objectToSpawn.transform.rotation = Quaternion.Euler(spawnRotation);
            objectToSpawn.transform.SetParent(spawnParent);
            PoolsDictionary[objectPoolName.ToString()].Enqueue(objectToSpawn);
            objectToSpawn.SetActive(true);
            var pulledObjectInterface = objectToSpawn.GetComponent<B_OPS_IPooledObject>();
            if (pulledObjectInterface != null) pulledObjectInterface.OnObjectSpawn();
            return objectToSpawn;
        }

        public ObjectsToPool GetObjectPool(string poolName) {
            return PoolsList.Find(t => t.PoolName == poolName);
        }

        public ObjectsToPool GetObjectPool(object poolName) {
            return PoolsList.Find(t => t.PoolName == poolName.ToString());
        }
        // public string PoolerName;
        [Serializable]
        public class ObjectsToPool {
            public string PoolName;
#if UNITY_EDITOR

            [AssetsOnly]
#endif
            public GameObject ObjectPrefab;

            public int PrewarmCount;
        }
    }
}