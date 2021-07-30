using UnityEngine;

namespace Herkdess.Tools.General
{
    public abstract class OPS_PooledObjectBase : MonoBehaviour, IPooledObject
    {
        public virtual void OnFirstSpawn()
        {
            Debug.Log(this.gameObject.name + " Has spawned for the first time and ready for normal use");
        }

        public virtual void OnObjectSpawn()
        {
            Debug.Log(this.gameObject.name + " Has spawned normally");
        }

        public virtual void OnRespawn()
        {
            Debug.Log(this.gameObject.name + " Has respawned normally");
        }
    }
}