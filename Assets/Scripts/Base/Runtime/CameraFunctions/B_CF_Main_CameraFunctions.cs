using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base
{
    public enum ActiveVirtualCameras { VirCam1, VirCam2, VirCam3 }

    public class B_CF_Main_CameraFunctions : MonoBehaviour
    {
        #region Properties

        public static B_CF_Main_CameraFunctions instance;

        [FoldoutGroup("VirtualCameras")]
        public VirCam VirtualCamera1;
        [FoldoutGroup("VirtualCameras")]
        public VirCam VirtualCamera2;
        [FoldoutGroup("VirtualCameras")]
        public VirCam VirtualCamera3;

        private Dictionary<ActiveVirtualCameras, VirCam> VirtualCameras;

        #endregion

        #region Unity Functions
        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(this.gameObject);
        }

        private void OnDisable()
        {
            instance = null;
        }


        #endregion

        #region Spesific Functions

        public void CameraFuncitonsStrapping()
        {
            VirtualCameras = new Dictionary<ActiveVirtualCameras, VirCam>();
            VirtualCameras.Add(ActiveVirtualCameras.VirCam1, VirtualCamera1);
            VirtualCameras.Add(ActiveVirtualCameras.VirCam2, VirtualCamera2);
            VirtualCameras.Add(ActiveVirtualCameras.VirCam3, VirtualCamera3);
            foreach (var item in VirtualCameras)
                item.Value.SetupVirtualCamera();
            B_CES_CentralEventSystem.OnLevelDisable.AddFunction(FlushData, true);
        }

        #endregion

        #region Generic Functions

        public void VirtualCameraSetAll(ActiveVirtualCameras Camera, Transform Target)
        {
            SwitchToCamera(Camera);
            VirtualCameras[Camera].VirtualCamera.Follow = Target;
            VirtualCameras[Camera].VirtualCamera.LookAt = Target;
        }

        public void VrtualCameraSetFollow(ActiveVirtualCameras Camera, Transform Target)
        {
            SwitchToCamera(Camera);
            VirtualCameras[Camera].VirtualCamera.Follow = Target;
        }

        public void VirtualCameraSetAim(ActiveVirtualCameras Camera, Transform Target)
        {
            SwitchToCamera(Camera);
            VirtualCameras[Camera].VirtualCamera.LookAt = Target;
        }

        public void SwitchToCamera(ActiveVirtualCameras Camera, Transform Target = null)
        {
            foreach (var item in VirtualCameras)
                item.Value.VirtualCamera.Priority = 9;

            VirtualCameras[Camera].VirtualCamera.Priority = 15;
            VirtualCameras[Camera].VirtualCamera.Follow = Target;
            VirtualCameras[Camera].VirtualCamera.LookAt = Target;
        }

        public void VirtualCameraShake(ActiveVirtualCameras Camera, float Amp, float TimeToShake)
        {
            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(VirtualCameras[Camera].coroutine, Shaker(Camera, Amp, TimeToShake));
        }

        public void ChangeCameraFOW(ActiveVirtualCameras Camera, float To, float Speed)
        {
            var cam = VirtualCameras[Camera].VirtualCamera;
            B_CR_CoroutineRunner.instance.CQ.EnqueueAction(Ieum_ChangeCameraFOW(cam, To, Speed));
        }

        private void FlushData()
        {
            foreach (var item in VirtualCameras)
                item.Value.FlushData();
        }
        #endregion

        #region IEnumerators
        private IEnumerator Ieum_ChangeCameraFOW(CinemachineVirtualCamera Camera, float To, float Speed)
        {
            while (Camera.m_Lens.FieldOfView != To)
            {
                Camera.m_Lens.FieldOfView = Mathf.MoveTowards(Camera.m_Lens.FieldOfView, To, Speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator Shaker(ActiveVirtualCameras Camera, float Amp, float TimeForShake)
        {
            CinemachineBasicMultiChannelPerlin perlin = VirtualCameras[Camera].VirtualCameraPerlinChannel;
            float TimeDiff = Amp / TimeForShake;
            perlin.m_AmplitudeGain = Amp;
            perlin.m_FrequencyGain = 1;
            while (TimeForShake != 0)
            {
                TimeForShake = Mathf.MoveTowards(TimeForShake, 0, Time.deltaTime);
                perlin.m_AmplitudeGain = Mathf.MoveTowards(perlin.m_AmplitudeGain, VirtualCameras[Camera].DefaultAmp.x, TimeDiff * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            TimeForShake = 0;
            perlin.m_AmplitudeGain = VirtualCameras[Camera].DefaultAmp.x;
            perlin.m_FrequencyGain = VirtualCameras[Camera].DefaultAmp.y;
        }
        #endregion
















    }

    [System.Serializable]
    public class VirCam
    {
        [DisableIf("@Locked")]
        public CinemachineVirtualCamera VirtualCamera;

        [DisableIf("@Locked")]
        public Vector2 DefaultAmp;

        [DisableIf("@Locked")]
        [HideIf("@ResetOnlyLens")]
        public bool ResetOnLoad = false;

        [DisableIf("@Locked")]
        [HideIf("@ResetOnLoad")]
        public bool ResetOnlyLens = false;

        [HideInInspector] public Coroutine coroutine;
        [HideInInspector] public CinemachineBasicMultiChannelPerlin VirtualCameraPerlinChannel;
        private LensSettings LensSettings;
        private Vector3 OriginalPosition;
        private Quaternion OriginalRotation;
        private float OriginalFieldOfView;

        private bool Locked;

        public void SetupVirtualCamera()
        {
            LensSettings = VirtualCamera.m_Lens;
            VirtualCameraPerlinChannel = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            OriginalPosition = VirtualCamera.transform.position;
            OriginalRotation = VirtualCamera.transform.rotation;
            OriginalFieldOfView = VirtualCamera.m_Lens.FieldOfView;
            coroutine = null;
        }

        public void FlushData()
        {
            if (ResetOnLoad)
            {
                VirtualCamera.transform.position = OriginalPosition;
                VirtualCamera.transform.rotation = OriginalRotation;
                VirtualCamera.m_Lens.FieldOfView = OriginalFieldOfView;
                VirtualCamera.m_Lens = LensSettings;
            }
        }

        [ShowIf("@Locked == false")]
        [Button]
        public void LockSettings()
        {
            Locked = true;
        }

        [ShowIf("@Locked == true")]
        [Button]
        public void UnlockSettings()
        {
            Locked = false;
        }
    }
}