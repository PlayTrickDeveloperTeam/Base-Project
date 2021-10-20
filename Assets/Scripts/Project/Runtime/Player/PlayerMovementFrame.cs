using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace Base
{
    public class PlayerMovementFrame : PlayerSubFrame
    {
        #region Properties

        [HideInInspector] public LeanDragTranslate LeanTranslate;
        public float ForwardSpeed;
        [Range(.1f, 3f)]
        public float SidewaySpeed;
        public Vector2 MovementClamp;
        Vector3 MoveVector;

        bool canMove;
        public override bool CanAct
        {
            get
            {
                return base.CanAct && canMove;
            }
        }

        #endregion

        #region Unity Functions

        private void Start()
        {
            SetupSubFrame();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        #endregion

        #region Spesific Functions

        public override void SetupSubFrame()
        {
            base.SetupSubFrame();
            Parent.MovementFrame = this;
            LeanTranslate = GetComponent<LeanDragTranslate>();
            LeanTranslate.Sensitivity = SidewaySpeed;
            LeanTranslate.MovementClamp = MovementClamp;
        }

        public override void Go()
        {
            base.Go();
            UpdateAction += MoveBody;
            canMove = true;
            LeanTranslate.CanMove = canMove;
        }

        public override void EndFunctions()
        {
            LeanTranslate.CanMove = false;
            base.EndFunctions();
        }

        void MoveBody()
        {
            MoveVector = transform.position;
            MoveVector = transform.forward.normalized * ForwardSpeed;
            transform.Translate(MoveVector * Time.deltaTime);
        }

        #endregion

        #region Generic Functions

        #endregion

        #region IEnumerators

        #endregion
    }
}