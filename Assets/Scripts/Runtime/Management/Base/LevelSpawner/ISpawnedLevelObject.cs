using System.Collections;
using UnityEngine;

namespace Main
{
    public interface ISpawnedLevelObject
    {
        public void OnLevelAwake();
        public void OnLevelOnEnable();
        public void OnLevelStart();
        public void OnLevelInitate();
        public void OnLevelCommand();
    }
}