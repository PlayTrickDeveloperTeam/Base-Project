using System;
using UnityEngine;
namespace Base {
    public abstract class PlayerSubFrame : MonoBehaviour {
        #region Properties

        protected PlayerMainframe Parent;
        protected Action UpdateAction;
        protected Action FixedUpdateAction;

        public virtual bool CanAct => B_GM_GameManager.instance.IsGamePlaying() && Parent.SetupComplete;

        #endregion

        #region Unity Functions

        public virtual void Update() {
            if (UpdateAction == null) return;
            if (!CanAct) return;
            UpdateAction?.Invoke();
        }
        public virtual void FixedUpdate() {
            if (FixedUpdateAction == null) return;
            if (!CanAct) return;
            FixedUpdateAction?.Invoke();
        }

        #endregion

        #region Spesific Functions

        public virtual void SetupSubFrame() {
            Parent = GetComponent<PlayerMainframe>();
            Parent.AddFramesToList(this);
            //B_CES_CentralEventSystem.OnBeforeLevelEnd.AddFunction(EndFunctions, false);
        }

        public virtual void Go() { }

        public virtual void EndFunctions() {
            UpdateAction = null;
            FixedUpdateAction = null;
        }

        #endregion

        #region Generic Functions

        #endregion

        #region IEnumerators

        #endregion
    }
}