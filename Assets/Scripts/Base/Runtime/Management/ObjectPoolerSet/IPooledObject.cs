using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Herkdess.Tools.General
{
    public interface IPooledObject
    {
        public void OnFirstSpawn();
        public void OnObjectSpawn();
        public void OnRespawn();
    }
}