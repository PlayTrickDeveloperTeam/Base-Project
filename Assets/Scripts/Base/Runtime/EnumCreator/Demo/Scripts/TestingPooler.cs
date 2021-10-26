using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class TestingPooler : B_OPS_Pooler_Base
    {
        private void Awake()
        {
        }
        private void Start()
        {
            base.InitiatePooller();
            for (int i = 0; i < 10; i++)
                SpawnObjFromPool(DemoPoolerTester.Lar, Vector3.zero);
        }
    }
}