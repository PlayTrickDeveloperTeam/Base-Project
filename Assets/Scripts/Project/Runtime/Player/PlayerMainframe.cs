using System.Collections.Generic;
using UnityEngine;
namespace Base {
    [DefaultExecutionOrder(-10)]
    public class PlayerMainframe : MonoBehaviour {

        #region Unity Functions

        private void Start() {
            PlayerSetup();
        }

        #endregion Unity Functions
        
        #region Properties

        #endregion Properties

        #region Main Frame

        #region Properties

        private List<PlayerSubFrame> SubFrames;
        [HideInInspector] public PlayerMovementFrame MovementFrame;
        [HideInInspector] public PlayerScoreframe ScoreFrame;
        [HideInInspector] public PlayerTriggerEvents TriggerEvents;
        public bool SetupComplete;

        #endregion Properties

        private void PlayerSetup() {
            SubFrames = new List<PlayerSubFrame>();
            B_CES_CentralEventSystem.BTN_OnStartPressed.AddFunction(Go, false);
            B_CF_Main_CameraFunctions.instance.VirtualCameraSetAll(ActiveVirtualCameras.VirCam1, transform);
            SetupComplete = true;
        }

        private void Go() {
            SubFrames.ForEach(t => t.Go());
        }

        public void EndGameFunction(bool success) {
            B_GM_GameManager.instance.ActivateEndgame(success, 2);
            SubFrames.ForEach(t => t.EndFunctions());
        }

        public void AddFramesToList(PlayerSubFrame SubFrame) {
            SubFrames.Add(SubFrame);
        }

        #endregion Main Frame

        #region Spesific Functions

        #endregion Spesific Functions

        #region Generic Functions

        #endregion Generic Functions
    }
}