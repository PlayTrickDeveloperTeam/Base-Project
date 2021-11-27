using System;
namespace Base {
    public class PlayerMovementFrame : PlayerSubFrame {
        #region Properties

        public PlayerPathFollerSubframe PathFollerSubframe;
        public Action<bool> EndReached;

        private bool canMove;
        public override bool CanAct => base.CanAct && canMove;

        #endregion

        #region Unity Functions

        private void Start() {
            SetupSubFrame();
        }

        public override void Update() {
            base.Update();
        }

        public override void FixedUpdate() {
            base.FixedUpdate();
        }

        #endregion

        #region Spesific Functions

        public override void SetupSubFrame() {
            base.SetupSubFrame();
            PathFollerSubframe.Setup(transform, this);
            Parent.MovementFrame = this;
            EndReached += Parent.EndGameFunction;
        }

        public override void Go() {
            base.Go();
            UpdateAction += PathFollerSubframe.MoveBody;
            canMove = true;
        }

        public override void EndFunctions() {
            base.EndFunctions();
        }

        #endregion

        #region Generic Functions

        #endregion

        #region IEnumerators

        #endregion
    }
}