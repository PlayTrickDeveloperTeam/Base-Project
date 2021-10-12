using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
namespace Base
{
    public class B_CF_Main_CameraFunctions : MonoBehaviour
    {
        public static B_CF_Main_CameraFunctions instance;

        public CinemachineVirtualCamera VirCam1;
        public CinemachineVirtualCamera VirCam2;
        public CinemachineVirtualCamera VirCam3;

        CinemachineBasicMultiChannelPerlin ChannelPerlin;
        CinemachineBasicMultiChannelPerlin ChannelPerlin2;
        CinemachineBasicMultiChannelPerlin ChannelPerlin3;

        Coroutine ShakeRoutine;
        Coroutine ShakeRoutine2;
        Coroutine ShakeRoutine3;

        Coroutine Cam1Backup;
        Coroutine Cam2Backup;
        Coroutine Cam3Backup;

        //B_CR_CoroutineQueue CQ;

        public Vector2 DefaultWalkAmp;

        Vector3 cam1Pos;
        Vector3 cam2Pos;
        Vector3 cam3Pos;
        Quaternion cam1Rot;
        Quaternion cam2Rot;
        Quaternion cam3Rot;

        CinemachineTransposer VirCam1Transposer;
        CinemachineTransposer VirCam2Transposer;
        CinemachineTransposer VirCam3Transposer;

        float OriginalFov;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this.gameObject);
        }

        public void CameraFuncitonsStrapping()
        {
            ChannelPerlin = VirCam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            ChannelPerlin2 = VirCam2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            ChannelPerlin3 = VirCam3.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            VirCam1Transposer = VirCam1.GetCinemachineComponent<CinemachineTransposer>();
            VirCam2Transposer = VirCam2.GetCinemachineComponent<CinemachineTransposer>();
            VirCam3Transposer = VirCam3.GetCinemachineComponent<CinemachineTransposer>();


            cam1Pos = VirCam1.transform.position;
            cam2Pos = VirCam2.transform.position;

            cam1Rot = VirCam1.transform.rotation;
            cam2Rot = VirCam2.transform.rotation;

            cam3Pos = VirCam3.transform.position;
            cam3Rot = VirCam3.transform.rotation;

            OriginalFov = VirCam1.m_Lens.FieldOfView;

            B_CES_CentralEventSystem.OnLevelDisable.AddFunction(FlushData, true);
        }

        void FlushData()
        {
            VirCam1.m_Lens.FieldOfView = OriginalFov;
            VirCam2.m_Lens.FieldOfView = 60;
            VirCam3.m_Lens.FieldOfView = 60;
        }


        public void ShakeCameraStart(float amp, float time)
        {
            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(ShakeRoutine, Shaker(amp, time));
        }

        public void ShakeCameraStart2(float amp, float time)
        {
            //RageCam();
            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(ShakeRoutine2, Shaker2(amp, time));
        }

        public void ShakeCameraStart3(float amp, float time)
        {

            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(ShakeRoutine3, Shaker3(amp, time));
        }

        void StartHeadBobbing()
        {
            if (ShakeRoutine != null)
                B_CR_CoroutineRunner.instance.CQ.StopLoop();
            ChannelPerlin.m_AmplitudeGain = DefaultWalkAmp.x;
            ChannelPerlin.m_FrequencyGain = DefaultWalkAmp.y;
        }

        void StopHeadBobbing()
        {
            ChannelPerlin.m_AmplitudeGain = 0;
            ChannelPerlin.m_FrequencyGain = 0;
        }

        public void ManipulateVirCam1Z(Vector3 pos, float speed)
        {
            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(Cam1Backup, CameraBackup(VirCam1Transposer, pos, speed));
        }

        public void ManipulateVirCam2Z(Vector3 pos, float speed)
        {
            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(Cam2Backup, CameraBackup(VirCam2Transposer, pos, speed));
        }
        public void ManipulateVirCam3Z(Vector3 pos, float speed)
        {
            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(Cam3Backup, CameraBackup(VirCam3Transposer, pos, speed));
        }

        IEnumerator CameraBackup(CinemachineTransposer transposer, Vector3 pos1, float speedStep)
        {
            Vector3 pos = transposer.m_FollowOffset;
            pos += pos1;
            while (Vector3.Distance(pos, transposer.m_FollowOffset) > .1f)
            {
                transposer.m_FollowOffset = Vector3.Slerp(transposer.m_FollowOffset, pos, speedStep * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            transposer.m_FollowOffset = pos;
        }

        public void ModifyVirCamFov(CinemachineVirtualCamera cam, float to, float speed)
        {
            B_CR_CoroutineRunner.instance.CQ.EnqueueAction(ChangeCameraFov(cam, to, speed));
        }

        IEnumerator ChangeCameraFov(CinemachineVirtualCamera cam, float to, float speed)
        {
            while (cam.m_Lens.FieldOfView != to)
            {
                cam.m_Lens.FieldOfView = Mathf.MoveTowards(cam.m_Lens.FieldOfView, to, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            //Bölüm sonunda kamerayı sıfırla
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

        IEnumerator Shaker2(float amp, float time)
        {
            float TimeDiff = amp / time;
            ChannelPerlin2.m_AmplitudeGain = amp;
            ChannelPerlin2.m_FrequencyGain = 1;
            while (time != 0)
            {
                time = Mathf.MoveTowards(time, 0, Time.deltaTime);
                ChannelPerlin2.m_AmplitudeGain = Mathf.MoveTowards(ChannelPerlin2.m_AmplitudeGain, DefaultWalkAmp.x, TimeDiff * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            time = 0;
            ChannelPerlin2.m_AmplitudeGain = DefaultWalkAmp.x;
            ChannelPerlin2.m_FrequencyGain = DefaultWalkAmp.y;
        }

        IEnumerator Shaker3(float amp, float time)
        {
            float TimeDiff = amp / time;
            ChannelPerlin3.m_AmplitudeGain = amp;
            ChannelPerlin3.m_FrequencyGain = 1;
            while (time != 0)
            {
                time = Mathf.MoveTowards(time, 0, Time.deltaTime);
                ChannelPerlin3.m_AmplitudeGain = Mathf.MoveTowards(ChannelPerlin3.m_AmplitudeGain, DefaultWalkAmp.x, TimeDiff * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            time = 0;
            ChannelPerlin3.m_AmplitudeGain = DefaultWalkAmp.x;
            ChannelPerlin3.m_FrequencyGain = DefaultWalkAmp.y;
        }

        private void OnDisable()
        {
            instance = null;
        }

        public void VirCam1SetAll(Transform target)
        {
            VirCam1.Follow = target;
            VirCam1.LookAt = target;
        }

        public void VirCam2SetAll(Transform target)
        {
            VirCam2.Follow = target;
            VirCam2.LookAt = target;
        }

        public void VirCam3SetAll(Transform target)
        {
            VirCam3.Follow = target;
            VirCam3.LookAt = target;
        }

        public void VirCam1SetFollow(Transform target) => VirCam1.Follow = target;
        public void VirCam1SetAim(Transform target) => VirCam1.LookAt = target;
        public void VirCam2SetFollow(Transform target) => VirCam2.Follow = target;
        public void VirCam2SetAim(Transform target) => VirCam2.LookAt = target;
        public void VirCam3SetFollow(Transform target) => VirCam3.Follow = target;
        public void VirCam3SetAim(Transform target) => VirCam3.LookAt = target;

        public void SwitchV1Cam(Transform target)
        {
            //VirCam1.LookAt = target;
            VirCam1.Follow = target;
            VirCam1.Priority = 10;
            VirCam2.Priority = 9;
            VirCam3.Priority = 8;
        }
        public void SwitchV2Cam(Transform target)
        {
            //VirCam2.transform.position = new Vector3(target.position.x, target.position.y + 5, target.position.z - 10);
            VirCam3SetAll(null);
            VirCam2.LookAt = target;
            VirCam1.Priority = 9;
            VirCam2.Priority = 10;
            VirCam3.Priority = 8;
        }

        public void SwitchV3Cam(Transform target)
        {
            VirCam3.LookAt = target;
            VirCam1SetAll(null);
            VirCam1.Priority = 8;
            VirCam2.Priority = 9;
            VirCam3.Priority = 10;
        }

        void RageCam()
        {
            StartCoroutine(RageIt());
        }

        IEnumerator RageIt()
        {
            float aim1 = 40, aim2 = 60;
            float speed = 240;
            float FOW = VirCam2.m_Lens.FieldOfView;
            for (int i = 0; i < 2; i++)
            {
                while (FOW != aim1)
                {
                    FOW = Mathf.MoveTowards(FOW, aim1, speed * Time.deltaTime);
                    VirCam2.m_Lens.FieldOfView = FOW;
                    yield return new WaitForEndOfFrame();
                }
                while (FOW != aim2)
                {
                    FOW = Mathf.MoveTowards(FOW, aim2, speed * Time.deltaTime);
                    VirCam2.m_Lens.FieldOfView = FOW;
                    yield return new WaitForEndOfFrame();
                }
                aim1 += 10;
            }
        }
    }
}
