using System.Collections;
using UnityEngine;
using Herkdess.Tools.General;
using UnityEngine.Events;

namespace Base
{
    public class Example_SpawnedCube : B_OPS_PooledObjectBase
    {
        public UnityEvent OnFirstSpawnEvent;
        public override void OnFirstSpawn()
        {
            OnFirstSpawnEvent?.Invoke();
            base.OnFirstSpawn();
        }

        public override void OnObjectSpawn()
        {
            ChangeColor();
            base.OnObjectSpawn();
        }

        public override void OnRespawn()
        {
            base.OnRespawn();
        }

        void ChangeColor()
        {
            Material mat = transform.GetComponent<MeshRenderer>().material;
            mat.color = UnityEngine.Random.ColorHSV();
        }

    }
}
