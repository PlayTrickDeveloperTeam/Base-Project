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
            PlayerSetup();
        }

        #endregion Unity Functions

        #region Spesific Functions

        private void PlayerSetup()
        {
            B_MM_MenuManager_Project.instance.OnPickupTaken += OnPickupTaken;
            ScoreFrame = GetComponent<PlayerScoreframe>();
        }

        public void OnPickupTaken(float Value)
        {
            ScoreFrame.OnPickupTaken(Value);
        }

        #endregion Spesific Functions
    }
}