using UnityEngine;

namespace Base
{
    public class PlayerMainframe : MonoBehaviour
    {
        #region Properties

        private PlayerScoreframe ScoreFrame;

        #endregion Properties

        #region Unity Functions

        private void Start()
        {
            B_CES_CentralEventSystem.BTN_OnStartPressed.AddFunction(PlayerSetup, false);
            Lean.Touch.LeanTouch.OnFingerDown += ShakeTest;
        }

        #endregion Unity Functions

        #region Spesific Functions

        private void PlayerSetup()
        {
            B_MM_MenuManager_Project.instance.OnPickupTaken += OnPickupTaken;
            ScoreFrame = GetComponent<PlayerScoreframe>();
            B_CF_Main_CameraFunctions.instance.VirtualCameraSetAll(ActiveVirtualCameras.VirCam1, transform);
        }

        void ShakeTest(Lean.Touch.LeanFinger finger)
        {
            B_CF_Main_CameraFunctions.instance.VirtualCameraShake(ActiveVirtualCameras.VirCam1, 12, 3);
        }

        public void OnPickupTaken(float Value)
        {
            ScoreFrame.OnPickupTaken(Value);
        }

        #endregion Spesific Functions
    }
}