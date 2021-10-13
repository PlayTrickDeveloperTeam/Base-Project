using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public class PlayerMainframe : MonoBehaviour
    {
        #region Properties

        PlayerScoreframe ScoreFrame;

        #endregion

        #region Unity Functions

        private void Start()
        {
            PlayerSetup();
        }

        #endregion

        #region Spesific Functions

        void PlayerSetup()
        {
            B_MM_MenuManager_Project.instance.OnPickupTaken += OnPickupTaken;
            ScoreFrame = GetComponent<PlayerScoreframe>();

        }

        public void OnPickupTaken(float Value)
        {
            ScoreFrame.OnPickupTaken(Value);
        }

        #endregion

        #region Generic Functions

        #endregion
    }
}