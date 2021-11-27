namespace Base {
    public class PlayerScoreframe : PlayerSubFrame {
        #region Properties

        public float PlayerScore;

        #endregion Properties

        #region Unity Functions

        private void Start() {
            SetupSubFrame();
        }

        #endregion Unity Functions

        #region Spesific Functions

        public void SetupScoreFrame() {
            Parent.AddFramesToList(this);
        }

        public void OnPickupTaken(float value) { }

        public override void SetupSubFrame() {
            base.SetupSubFrame();
            Parent.ScoreFrame = this;
        }

        #endregion Spesific Functions

        #region Generic Functions

        #endregion

        #region IEnumerators

        #endregion
    }
}