using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
public class VFMTester : MonoBehaviour
{
    void Start() {
        EffectsManager.SpawnAParticle(Enum_Particles.CubeExplosion, Vector3.zero).SetLoop(5);
    }
    
}
