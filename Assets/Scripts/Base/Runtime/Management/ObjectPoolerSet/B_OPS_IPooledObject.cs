using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Base
{
    public interface B_OPS_IPooledObject
    {
        public void OnFirstSpawn();
        public void OnObjectSpawn();
        public void OnRespawn();
    }
}