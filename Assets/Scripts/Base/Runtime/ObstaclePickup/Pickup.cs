using UnityEngine;
namespace Base {
    public class Pickup : MonoBehaviour, ICollectable {
        public float Value;

        public void OnPickup(float value) { }
    }
}