using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base
{
    public class B_VFM_EffectsManager : MonoBehaviour
    {
        private List<GameObject> particles = new List<GameObject>();
        private List<Particle> spawnedParticles = new List<Particle>();
        public static B_VFM_EffectsManager instance;

        [SerializeField] private Enum_Particles particlesEnum;
        private float spawnDelay;
        private bool loop;
        private Transform parent;
        private Vector3 newScale;
        private Quaternion rot;
        
#if UNITY_EDITOR
        [Button("Save Enums")]
        void OnObjectsPoolManipulated()
        {
            List<string> particleNames = new List<string>();
            
            foreach (GameObject g in Resources.LoadAll("Particles", typeof(GameObject)))
            {
                particleNames.Add(g.name);
            }
            
            string[] Names = new string[particleNames.Count];
            for (int i = 0; i < particleNames.Count; i++)
            {
                Names[i] = particleNames[i];
            }
            EnumCreator.CreateEnum("Particles", Names);
        }
#endif

        private void ResOptions()
        {
            spawnDelay = 0;
            loop = false;
            parent = null;
            newScale = Vector3.one;
            rot = Quaternion.identity;
        }
        
        private void OnDisable()
        {
            instance = null;
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            
            Load();
        }

        private void Load()
        {
            List<string> particleNames = new List<string>();
            
            foreach (GameObject g in Resources.LoadAll("Particles", typeof(GameObject)))
            {
                particleNames.Add(g.name);
            }
            
        }
        

        private B_VFM_EffectsManager SetDelay(int SetDelay)
        {
            spawnDelay = SetDelay;
            return this;
        }

        private B_VFM_EffectsManager SetLoop(bool setValue)
        {
            loop = setValue;
            return this;
        }

        private B_VFM_EffectsManager SetParent(Transform setValue)
        {
            parent = setValue;
            return this;
        }
        
        private B_VFM_EffectsManager SetScale(Vector3 setValue)
        {
            newScale = setValue;
            return this;
        }

        private B_VFM_EffectsManager SetRot(Quaternion setValue)
        {
            rot = setValue;
            return this;
        }
        
        public async void SpawnParticle(string particleName,Vector3 pos,[Optional] Quaternion rot)
        {
            GameObject selected = null;
            
            foreach (var particle in particles)
            {
                if (particle.name.ToLower().Equals(particleName.ToLower()))
                {
                    selected = particle;
                    break;
                }
            }

            if (selected == null)
            {
                Debug.LogWarning("CANT FINDED PARTICLE");
                return;
            }
            
            await Task.Delay((int)(spawnDelay * 1000));
            
            var spawned = Instantiate(selected);
            spawned.SetActive(false);
            var particleScript = spawned.AddComponent<Particle>();
            spawnedParticles.Add(particleScript);
            
            particleScript.Initialize(parent,pos,rot,loop);
            ResOptions();
        }
        
    }

    public class Particle : MonoBehaviour
    {
        public string Id;
        private ParticleSystem _particleSystem;
        
        // private Transform parent;
        // private Vector3 pos;
        // private Quaternion rot;
        // private bool loop;
        
        public void Initialize(Transform parent,Vector3 pos,Quaternion rot,bool loop)
        {
            // this.parent = parent;
            // this.pos = pos;
            // this.rot = rot;
            // this.loop = loop;
            
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Stop();
            
            if (Id.Equals(""))
            {
                Id = gameObject.GetInstanceID().ToString();
            }
            
            var main = _particleSystem.main;
            
            main.loop = loop;
            
            if (parent != null)
            {
                transform.SetParent(parent);
            }

            transform.position = pos;
            transform.rotation = rot;
            
            _particleSystem.Play();
        }
    }
}