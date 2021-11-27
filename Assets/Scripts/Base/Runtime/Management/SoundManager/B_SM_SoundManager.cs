using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Base {
    [Serializable]
    public class B_SM_SoundManager : MonoBehaviour {
        public static B_SM_SoundManager instance;
        public SoundHolder[] Sounds;
        [HideInInspector] public List<AudioSource> Musics = new List<AudioSource>();
        [HideInInspector] public List<AudioSource> VFX = new List<AudioSource>();
        public SoundSettings SoundSettings = new SoundSettings();
        [HideInInspector] public float MusicVolume;
        [HideInInspector] public float VFXVolume;
        private Coroutine PlayLoopRoutine;

        private void Awake() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private void OnDisable() {
            instance = null;
        }

        public void PlaySound(string name) {
            var sound = Array.Find(Sounds, sound => sound.Name == name);
            if (sound == null) return;
            if (sound.AudioSource.isPlaying) {
                sound.AudioSource.Stop();
                sound.AudioSource.Play();
                return;
            }
            sound.AudioSource.Play();
        }

        public void PlayOnLoop(string name) {
            var sound = Array.Find(Sounds, sound => sound.Name == name);
            if (sound == null) return;
            sound.AudioSource.loop = true;
            sound.AudioSource.Play();
        }

        public void PlayOnArray(string name) {
            var sound = Array.Find(Sounds, sound => sound.Name == name);
            if (PlayLoopRoutine != null) {
                StopCoroutine(PlayLoopRoutine);
                PlayLoopRoutine = null;
                PlayLoopRoutine = StartCoroutine(SoundArrayLoop(sound));
            }
            PlayLoopRoutine = StartCoroutine(SoundArrayLoop(sound));
        }

        private IEnumerator SoundArrayLoop(SoundHolder sound) {
            for (var i = 0; i < sound.AudioSources.Count; i++) {
                sound.AudioSources[i].Play();
                yield return new WaitUntil(() => sound.AudioSources[i].isPlaying == false);
            }
            PlayOnArray(sound.Name);
            yield return null;
        }

        public bool SoundManagerStrapping() {
            foreach (var Sound in Sounds) {
                if (Sound.AudioClip == null) {
                    foreach (var sound in Sound.AudioClips) {
                        var source = gameObject.AddComponent<AudioSource>();
                        Sound.AudioSources.Add(source);
                        source.clip = sound;
                        source.volume = Sound.Volume;
                        source.pitch = Sound.Pitch;
                        source.playOnAwake = false;
                        MusicVolume = Sound.Volume;
                        Musics.Add(source);
                    }
                    continue;
                }
                Sound.AudioSource = gameObject.AddComponent<AudioSource>();
                Sound.AudioSource.clip = Sound.AudioClip;
                Sound.AudioSource.volume = Sound.Volume;
                Sound.AudioSource.pitch = Sound.Pitch;
                Sound.AudioSource.playOnAwake = false;
                VFXVolume = Sound.Volume;
                VFX.Add(Sound.AudioSource);
            }
            return true;
        }
    }

    [Serializable]
    public class SoundHolder {
        public AudioClip AudioClip;
        public AudioClip[] AudioClips;
        public string Name;

        [Range(0f, 1f)]
        public float Volume = .2f;

        [Range(.3f, 1f)]
        public float Pitch;

        [HideInInspector] public AudioSource AudioSource;
        [HideInInspector] public List<AudioSource> AudioSources = new List<AudioSource>();

        private bool IsArray() {
            if (AudioClips.Length > 0) {
                AudioClip = null;
                return false;
            }
            return true;
        }
    }

    [Serializable]
    public class SoundSettings {
        public Slider MusicSlider;
        public Toggle MusicToggle;
        public Slider VFXSlider;
        public Toggle VFXToggle;

        public void TGLOnMusicToggle(bool MusicIsOn) { }

        public void TGLOnVFXToggle(bool VFXIsOn) { }

        public void SLDOnMusicSliderChange(float Volume) {
            if (!MusicToggle.isOn) return;
        }

        public void SLDOnVFXSliderChange(float Volume) {
            if (!VFXToggle.isOn) return;
        }
    }

    //public class DONTFORGET
    //{
    //    public void SetLevel(float sliderValue)
    //    {
    //        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    //    }
    //}
}