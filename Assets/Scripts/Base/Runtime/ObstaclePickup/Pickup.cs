using UnityEngine;

namespace Base
{
    public class Pickup : MonoBehaviour, ICollectable
    {
        public float Value;

        public void OnPickup(float value)
        {
            B_MM_MenuManager_Project.instance.OnPickupTaken?.Invoke(value);
        }
    }
}