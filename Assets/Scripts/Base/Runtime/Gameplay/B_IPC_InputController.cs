using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Base;
using System.Linq;
namespace Base
{
    [Serializable]
    public class B_IPC_InputController
    {
        [HideInInspector] public bool CanPlay = false;
        //Must add on begin actions as well
        public Action OnTapAction;
        public Action OnHoldingDownAction;
        public Action OnHoldAction;
        public Action OnDragAction;
        public Action OnUpAction;

        public KeyCode Mouse0;

        [HideInInspector] public float DeadZone = 50f;
        [HideInInspector] public float HoldTimeLimit;
        [HideInInspector] public float DragTimer = 0;
        int dragCounter = 0;

        [HideInInspector] public Vector3 MouseStartPosition;
        [HideInInspector] public Vector3 MouseCurrentPosition;
        [HideInInspector] public Vector3 MouseEndPosition;
        [HideInInspector] public Vector3 SwipeDelta;

        [HideInInspector] public Vector3[] MousePositions;


        public B_IPC_InputController(MonoBehaviour parent, float activationDelay, float holdTimeLimit)
        {
            Mouse0 = KeyCode.Mouse0;
            this.HoldTimeLimit = holdTimeLimit;
            B_CES_CentralEventSystem.BTN_OnStartPressed.AddFunction(ActivateInputController, false);
            B_CR_CoroutineRunner.instance.CQ.RunFunctionWithDelay(ActivateInputController, activationDelay);
            MousePositions = new Vector3[20];
        }

        void ActivateInputController()
        {
            CanPlay = true;
        }

        public void Run()
        {
            if (!B_GM_GameManager.instance.IsGamePlaying()) return;
            if (!CanPlay) return;

            if (Input.GetKeyDown(Mouse0))
            {
                MouseStartPosition = Input.mousePosition;
                DragTimer = 0;
                OnTapAction?.Invoke();
            }
            if (Input.GetKey(Mouse0))
            {
                MouseCurrentPosition = Input.mousePosition;
                DragCounter();
                SwipeDelta = MouseStartPosition - MouseCurrentPosition;
                if (IsDeadZone())
                {
                    DragTimer = 0;
                    OnDragAction?.Invoke();
                }
                else
                {
                    DragTimer += 1 * Time.deltaTime;
                    if (DragTimer >= HoldTimeLimit)
                        OnHoldAction?.Invoke();
                    else
                    {
                        OnHoldingDownAction?.Invoke();
                    }
                }

            }
            if (Input.GetKeyUp(Mouse0))
            {
                DragTimer = 0;
                MouseEndPosition = Input.mousePosition;
                OnUpAction?.Invoke();
            }
        }

        void DragCounter()
        {
            if (dragCounter < MousePositions.Length)
            {
                MousePositions[dragCounter] = Input.mousePosition;
                //Debug.Log(MousePositions[internalcounterx]);
                dragCounter++;
            }
            else
            {
                MouseStartPosition = MousePositions[19];
                dragCounter = 0;
                MousePositions = new Vector3[20];
            }
        }

        public bool IsDeadZone()
        {
            if (SwipeDelta.magnitude > DeadZone)
                return true;
            return false;
        }

    }
}


