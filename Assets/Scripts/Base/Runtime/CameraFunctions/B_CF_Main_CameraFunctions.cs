using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Base {
    public enum ActiveVirtualCameras { VirCam1, VirCam2, VirCam3 }

    public class B_CF_Main_CameraFunctions : B_M_ManagerBase {
        #region Properties

        public static B_CF_Main_CameraFunctions instance;

        [FoldoutGroup("VirtualCameras")]
        public VirCam VirtualCamera1;

        [FoldoutGroup("VirtualCameras")]
        public VirCam VirtualCamera2;

        [FoldoutGroup("VirtualCameras")]
        public VirCam VirtualCamera3;

        private Dictionary<ActiveVirtualCameras, VirCam> VirtualCameras;

        #endregion Properties

        #region Unity Functions

        #endregion Unity Functions

        #region Spesific Functions

        public override Task ManagerStrapping() {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            VirtualCameras = new Dictionary<ActiveVirtualCameras, VirCam>();
            VirtualCameras.Add(ActiveVirtualCameras.VirCam1, VirtualCamera1);
            VirtualCameras.Add(ActiveVirtualCameras.VirCam2, VirtualCamera2);
            VirtualCameras.Add(ActiveVirtualCameras.VirCam3, VirtualCamera3);
            foreach (var item in VirtualCameras)
                item.Value.SetupVirtualCamera();
            B_CES_CentralEventSystem.OnLevelDisable.AddFunction(FlushData, true);
            return base.ManagerStrapping();
        }

        public override Task ManagerDataFlush() {
            instance = null;
            return base.ManagerDataFlush();
        }

        #endregion Spesific Functions

        #region Generic Functions

        public void VirtualCameraSetAll(ActiveVirtualCameras Camera, Transform Target) {
            SwitchToCamera(Camera);
            VirtualCameras[Camera].VirtualCamera.Follow = Target;
            VirtualCameras[Camera].VirtualCamera.LookAt = Target;
        }

        public void VrtualCameraSetFollow(ActiveVirtualCameras Camera, Transform Target) {
            SwitchToCamera(Camera);
            VirtualCameras[Camera].VirtualCamera.Follow = Target;
        }

        public void VirtualCameraSetAim(ActiveVirtualCameras Camera, Transform Target) {
            SwitchToCamera(Camera);
            VirtualCameras[Camera].VirtualCamera.LookAt = Target;
        }

        public void SwitchToCamera(ActiveVirtualCameras Camera, Transform Target = null) {
            foreach (var item in VirtualCameras)
                item.Value.VirtualCamera.Priority = 9;

            VirtualCameras[Camera].VirtualCamera.Priority = 15;
            if (Target) {
                VirtualCameras[Camera].VirtualCamera.Follow = Target;
                VirtualCameras[Camera].VirtualCamera.LookAt = Target;
            }
        }

        public void VirtualCameraShake(ActiveVirtualCameras Camera, float Amp, float TimeToShake) {
            B_CR_CoroutineRunner.instance.CQ.RunCoroutine(VirtualCameras[Camera].coroutine, Shaker(Camera, Amp, TimeToShake));
        }

        public void ChangeCameraFOW(ActiveVirtualCameras Camera, float To, float Speed) {
            var Cam = VirtualCameras[Camera].VirtualCamera;
            B_CR_CoroutineRunner.instance.CQ.EnqueueAction(Ieum_ChangeCameraFOW(Cam, To, Speed));
        }

        private void FlushData() {
            foreach (var item in VirtualCameras)
                item.Value.FlushData();
        }

        #endregion Generic Functions

        #region IEnumerators

        private IEnumerator Ieum_ChangeCameraFOW(CinemachineVirtualCamera Camera, float To, float Speed) {
            while (Camera.m_Lens.FieldOfView != To) {
                Camera.m_Lens.FieldOfView = Mathf.MoveTowards(Camera.m_Lens.FieldOfView, To, Speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator Shaker(ActiveVirtualCameras Camera, float Amp, float TimeForShake) {
            var perlin = VirtualCameras[Camera].VirtualCameraPerlinChannel;
            var TimeDiff = Amp / TimeForShake;
            perlin.m_AmplitudeGain = Amp;
            perlin.m_FrequencyGain = 1;
            while (TimeForShake != 0) {
                TimeForShake = Mathf.MoveTowards(TimeForShake, 0, Time.deltaTime);
                perlin.m_AmplitudeGain = Mathf.MoveTowards(perlin.m_AmplitudeGain, VirtualCameras[Camera].DefaultAmp.x, TimeDiff * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            TimeForShake = 0;
            perlin.m_AmplitudeGain = VirtualCameras[Camera].DefaultAmp.x;
            perlin.m_FrequencyGain = VirtualCameras[Camera].DefaultAmp.y;
        }

        #endregion IEnumerators
    }

    [Serializable]
    public class VirCam {
        [DisableIf("@Locked")]
        public CinemachineVirtualCamera VirtualCamera;

        [DisableIf("@Locked")]
        public Vector2 DefaultAmp;

        [DisableIf("@Locked")]
        [HideIf("@ResetOnlyLens")]
        public bool ResetOnLoad;

        [DisableIf("@Locked")]
        [HideIf("@ResetOnLoad")]
        public bool ResetOnlyLens;
        [HideInInspector] public CinemachineBasicMultiChannelPerlin VirtualCameraPerlinChannel;

        [HideInInspector] public Coroutine coroutine;
        private LensSettings LensSettings;

        private bool Locked = true;
        private float OriginalFieldOfView;
        private Vector3 OriginalPosition;
        private Quaternion OriginalRotation;

        public void SetupVirtualCamera() {
            LensSettings = VirtualCamera.m_Lens;
            VirtualCameraPerlinChannel = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            OriginalPosition = VirtualCamera.transform.position;
            OriginalRotation = VirtualCamera.transform.rotation;
            OriginalFieldOfView = VirtualCamera.m_Lens.FieldOfView;
            coroutine = null;
        }

        public void FlushData() {
            if (ResetOnLoad) {
                VirtualCamera.transform.position = OriginalPosition;
                VirtualCamera.transform.rotation = OriginalRotation;
                VirtualCamera.m_Lens.FieldOfView = OriginalFieldOfView;
                VirtualCamera.m_Lens = LensSettings;
            }
            else if (ResetOnlyLens) {
                VirtualCamera.m_Lens = LensSettings;
            }
        }

        [ShowIf("@Locked == false")]
        [Button]
        public void LockSettings() {
            Locked = true;
        }

        [ShowIf("@Locked == true")]
        [Button]
        public void UnlockSettings() {
            Locked = false;
        }
    }
}