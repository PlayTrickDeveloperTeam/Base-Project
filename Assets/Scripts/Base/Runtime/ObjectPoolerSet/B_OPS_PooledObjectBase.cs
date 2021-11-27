using UnityEngine;
namespace Base {
    public abstract class B_OPS_PooledObjectBase : MonoBehaviour, B_OPS_IPooledObject {
        public virtual void OnFirstSpawn() {
            //Debug.Log(this.gameObject.name + " Has spawned for the first time and ready for normal use");
        }

        public virtual void OnObjectSpawn() {
            //Debug.Log(this.gameObject.name + " Has spawned normally");
        }

        public virtual void OnRespawn() {
            //Debug.Log(this.gameObject.name + " Has respawned normally");
        }
    }
}