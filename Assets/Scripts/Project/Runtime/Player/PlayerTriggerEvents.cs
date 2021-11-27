using System;
using UnityEngine;
namespace Base {
    public class PlayerTriggerEvents : PlayerSubFrame {
        #region Properties

        public Action<object> OnPickup;
        public Action OnDeath;

        #endregion

        #region Unity Functions

        private void Start() {
            SetupSubFrame();
        }

        private void OnTriggerEnter(Collider other) { }

        #endregion

        #region Spesific Functions

        public override void SetupSubFrame() {
            base.SetupSubFrame();
            Parent.TriggerEvents = this;
        }

        public override void Go() {
            base.Go();
        }

        #endregion

        #region Generic Functions

        #endregion

        #region IEnumerators

        #endregion
    }
}