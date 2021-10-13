using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public interface ICollectable
    {
        void OnPickup(float value);
    }
}