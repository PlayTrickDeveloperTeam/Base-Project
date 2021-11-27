using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;
namespace Base {
    public static class EffectsManager {
        public static Task EffectsManagerStrapping() {
            // B_VFM_EffectsManager.instance.SpawnParticle("", Vector3.back).;
            return Task.CompletedTask;
        }

        public static PooledParticle SpawnAParticle(object enumToPull, Vector3 positionToSpawnIn, [Optional] Quaternion rotationToSpawnIn) {
            return B_VFM_EffectsManager.instance.SpawnAParticle(enumToPull, positionToSpawnIn);
        }
    }
}