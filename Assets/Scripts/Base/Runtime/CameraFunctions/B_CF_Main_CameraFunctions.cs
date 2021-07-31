using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

namespace Base
{
    public class B_CF_Main_CameraFunctions : MonoBehaviour
    {
        public static Action<float, float> ShakeCameraAction;
        CinemachineVirtualCamera VirtualCamera;
        CinemachineBasicMultiChannelPerlin ChannelPerlin;

        Coroutine ShakeRoutine;

        CoroutineQueue CQ;

        public float2 DefaultWalkAmp;

        private void Start()
        {
            VirtualCamera = GetComponent<CinemachineVirtualCamera>();
            ChannelPerlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            CQ = new CoroutineQueue(this);
            CQ.StartLoop();
            ShakeCameraAction += ShakeCameraStart;

            //If you have camera head bobbing use these
            //M_MenuManager_1.instance.Action_BTN_Start_Press += StartHeadBobbing;
            //M_MenuManager_1.instance.Action_Func_GameEnded += StopHeadBobbing;
        }


        void ShakeCameraStart(float amp, float time)
        {
            CQ.RunCoroutine(ShakeRoutine, Shaker(amp, time));
        }

        void StartHeadBobbing()
        {
            if (ShakeRoutine != null)
                CQ.StopLoop();
            ChannelPerlin.m_AmplitudeGain = DefaultWalkAmp.x;
            ChannelPerlin.m_FrequencyGain = DefaultWalkAmp.y;
        }

        void StopHeadBobbing()
        {
            ChannelPerlin.m_AmplitudeGain = 0;
            ChannelPerlin.m_FrequencyGain = 0;
        }

        IEnumerator Shaker(float amp, float time)
        {
            float TimeDiff = amp / time;
            ChannelPerlin.m_AmplitudeGain = amp;
            ChannelPerlin.m_FrequencyGain = 1;
            while (time != 0)
            {
                time = Mathf.MoveTowards(time, 0, Time.deltaTime);
                ChannelPerlin.m_AmplitudeGain = Mathf.MoveTowards(ChannelPerlin.m_AmplitudeGain, DefaultWalkAmp.x, TimeDiff * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            time = 0;
            ChannelPerlin.m_AmplitudeGain = DefaultWalkAmp.x;
            ChannelPerlin.m_FrequencyGain = DefaultWalkAmp.y;
        }

        private void OnDisable()
        {
            ShakeCameraAction = null;
        }
    }
}