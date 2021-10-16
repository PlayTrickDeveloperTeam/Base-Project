using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class RandomRotator : MonoBehaviour
    {
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, ModifiableValueManager.instance.ValueHolder.RunSpeed.ConnectedValue * Time.deltaTime));
        }
    }
}