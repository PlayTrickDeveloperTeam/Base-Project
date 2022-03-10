using System;
using Base;
using Base.UI;
using Dreamteck.Splines;
using Lean.Touch;
using UnityEngine;


[RequireComponent(typeof(SplineFollower))]
    public class RelativeFollower : MonoBehaviour
    {
        #region Fields
        
        private SplineFollower _follower;
        [SerializeField] private bool ignoreTranslate;
        public bool Move;
        public Vector2 Clamp;
        public float DeadZone;
        public float Speed;
        public float MoveSpeed;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _follower = GetComponent<SplineFollower>();
            _follower.followSpeed = 0;
        }

        private void Start()
        {
            GUIManager.GetButton(Enum_Menu_MainComponent.BTN_Start)
                .AddFunction(StartNow);
        }

        #endregion
        

        #region Methods
        private void StartNow()
        {
            _follower.followSpeed = MoveSpeed;
        }
        
        private float X;
        public void TranslatePosition(Vector2 magnitude)
        {
            if(!B_GM_GameManager.instance.IsGamePlaying()) return;
            if (Move)
            {
                var posX = magnitude.x * Speed;
                
                if (magnitude.x > DeadZone ||
                    magnitude.x < -DeadZone)
                {
                    X += posX;
                    
                    X = Mathf.Clamp(X,Clamp.x,Clamp.y);
                    
                    _follower.motion.offset = new Vector2(X,0);
                }
            }
        }

        #endregion
    }
