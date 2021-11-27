using UnityEngine;
namespace Base {
    public class RandomRotator : MonoBehaviour {
        private void Update() {
            transform.Rotate(new Vector3(0, 0, ModifiableValueManager.instance.ValueHolder.RunSpeed.ConnectedValue * Time.deltaTime));
        }
    }
}