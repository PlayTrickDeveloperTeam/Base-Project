using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Linq;
using Cinemachine.Utility;
using Sirenix.Utilities;


namespace Base
{
    public class B_VFM_EffectsManager : B_OPS_Pooler_Base
    {
        #region Variables

        public static B_VFM_EffectsManager instance;

        #endregion

        #region Main Functions

        public Task VFXManagerStrapping() {
            if (instance == null) 
                instance = this;
            else
                Destroy(this.gameObject);
            InitiatePooller(transform);
            return Task.CompletedTask;
        }
        
        public PooledParticle SpawnAParticle(object enumToPull, Vector3 positionToSpawnIn, [Optional] Quaternion rotationToSpawnIn) {
            GameObject obj = SpawnObjFromPool(enumToPull.ToString(), positionToSpawnIn, rotationToSpawnIn);
            return obj.GetComponent<PooledParticle>();
        }
        
        private void OnDisable()
        {
            instance = null;
        }

        #endregion

        #region Editor Functions

        #if UNITY_EDITOR
        
        [Button("Set Pooled Particles")]
        private void Load() {
            GameObject[] ObjectsInResources = Resources.LoadAll<GameObject>("Particles");
            List<string> particleNames = new List<string>();
            
            PoolsList = new List<ObjectsToPool>();
            PoolsDictionary = new Dictionary<string, Queue<GameObject>>();
            
            foreach (GameObject g in ObjectsInResources)
            {
                particleNames.Add(g.name);
                AddPoolInEditor(g, g.name,10);
            }
            EnumCreator.CreateEnum("Particles", particleNames.ToArray());
        }
  
#endif

        #endregion
        


    }

}