using System.Collections;
using UnityEngine;
using Herkdess.Tools.General;

namespace Base
{
    public class VFM_EffectsManager : OPS_Pooler_Base, ISpawnedLevelObject
    {
        public static VFM_EffectsManager instance;

        private void OnDisable()
        {
            instance = null;
        }

        public void OnLevelAwake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void OnLevelOnEnable()
        {
            base.InitiatePooller(this.transform);
        }

        public void OnLevelStart()
        {

        }

        public void OnLevelInitate()
        {

        }

        public void OnLevelCommand()
        {

        }
    }
}