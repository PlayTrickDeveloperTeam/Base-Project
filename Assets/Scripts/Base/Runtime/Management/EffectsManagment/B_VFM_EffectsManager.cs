using System.Collections;
using UnityEngine;
using Herkdess.Tools.General;

namespace Base
{
    public class B_VFM_EffectsManager : B_OPS_Pooler_Base, B_LC_ISpawnedLevelObject
    {
        public static B_VFM_EffectsManager instance;

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