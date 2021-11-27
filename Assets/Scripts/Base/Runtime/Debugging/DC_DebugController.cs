using UnityEngine;
using UnityEngine.UI;
namespace Herkdess.Tools.General.Debugging {
    public class DC_DebugController : MonoBehaviour {
        public static DC_DebugController instance;
        private int DebugCounter;
        private Text DebugText;

        private void Awake() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            DebugCounter = 0;
            //DebugText = GameObject.Find(B_Database_String.Text_Object_Debug).GetComponent<Text>();
        }

        private void OnDestroy() {
            instance = null;
        }

        public void DebugTracker(int Count) {
            DebugCounter++;
            Debug.Log("Phase " + DebugCounter + " Has Finished");
        }

        public void DebugTracker() {
            DebugCounter++;
            Debug.Log("Phase " + DebugCounter + " Has Finished");
            //DebugText.text = "Phase " + DebugCounter + " Has Finished";
        }
    }
}