using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace Base
{
    public class B_E_Player : MonoBehaviour
    {
        [SerializeField] B_IPC_InputController IPC;
        Slider SL;
        Image IM;


        //Needs more testing on events and actions
        //public UnityAction Actiontest;
        //public UnityEvent evet;
        private void Start()
        {
            IPC = new B_IPC_InputController(this, .5f, 3f);
            IPC.OnTapAction += OnTap;
            IPC.OnHoldingDownAction += OnHoldCount;
            IPC.OnHoldAction += OnHold;
            IPC.OnDragAction += OnDrag;
            IPC.OnUpAction += OnUp;
            SL = GameObject.Find("SL_Testing").GetComponent<Slider>();
            IM = GameObject.Find("Image_Testing").GetComponent<Image>();
            SL.minValue = 0; SL.maxValue = 100; SL.value = 0;
            IM.fillAmount = 0;
        }

        private void Update()
        {
            IPC.Run();
        }

        void OnTap()
        {
            SL.value = IPC.DragTimer;
            IM.fillAmount = 0;
            Debug.Log("Tapping");
        }

        void OnHoldCount()
        {
            SL.value = IPC.DragTimer.Remap(0f, 3f, 0f, 100f);
            IM.fillAmount = IPC.DragTimer.Remap(0f, 3f, 0f, 1f);
        }
        void OnHold()
        {

        }

        void OnDrag()
        {

        }
        void OnUp()
        {

        }
    }
}

