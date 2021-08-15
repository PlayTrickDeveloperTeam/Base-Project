using System.Collections;
using UnityEngine;
using Herkdess.Tools.General;

namespace Base
{
    public class B_VFM_EffectsManager : B_OPS_Pooler_Base
    {
        public static B_VFM_EffectsManager instance;

        private void OnDisable()
        {
            instance = null;
        }

        private void Awake()
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

        private void OnEnable()
        {
            base.InitiatePooller(this.transform);
        }

    }
}