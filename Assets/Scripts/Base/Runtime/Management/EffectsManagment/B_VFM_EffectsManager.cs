using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Base
{
    public class B_VFM_EffectsManager : B_OPS_Pooler_Base
    {
        private List<GameObject> particles = new List<GameObject>();
        private List<Particle> spawnedParticles = new List<Particle>();
        public static B_VFM_EffectsManager instance;

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
            foreach (GameObject g in Resources.LoadAll("Particles", typeof(GameObject)))
            {
                particles.Add(g);
            }
        }
        
        private int spawnDelay;
        private B_VFM_EffectsManager SetDelay(int SetDelay)
        {
            spawnDelay = SetDelay;
            return this;
        }

        private bool loop;
        private B_VFM_EffectsManager SetLoop(bool setValue)
        {
            loop = setValue;
            return this;
        }

        private Transform parent;
        private B_VFM_EffectsManager SetParent(Transform setValue)
        {
            parent = setValue;
            return this;
        }
        
        private Vector3 newScale;
        private B_VFM_EffectsManager SetScale(Vector3 setValue)
        {
            newScale = setValue;
            return this;
        }

        private Quaternion rot;
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
            

            await Task.Delay(spawnDelay * 1000);
            
            var spawned = Instantiate(selected);
            spawned.SetActive(false);
            var particleScript = spawned.AddComponent<Particle>();
            spawnedParticles.Add(particleScript);
            
            particleScript.Initialize(parent,pos,rot,loop);
        }

        private void OnEnable()
        {
            base.InitiatePooller(this.transform);
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