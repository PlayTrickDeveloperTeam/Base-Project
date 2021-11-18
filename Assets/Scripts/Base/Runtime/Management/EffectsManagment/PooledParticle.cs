using System.Threading.Tasks;
using UnityEngine;
namespace Base {
    public class PooledParticle : MonoBehaviour, B_OPS_IPooledObject
    {
        private ParticleSystem _particleSystem;

        private float _loopDelay;

        private void SetupParticle() {
            _particleSystem = GetComponent<ParticleSystem>();
            if (_particleSystem.isPlaying) 
                _particleSystem.Stop();
            
            this._loopDelay = _particleSystem.main.duration;
        }

        public PooledParticle PlayParticle() {
            _particleSystem.Play();
            return this;
        }

        public async Task<PooledParticle> SetLoop(int loopAmount) {
            for (int i = 0; i < loopAmount; i++) {
                _particleSystem.Stop();
                _particleSystem.Play();
                await Task.Delay((int)this._loopDelay * 1000);
            }
            return this;
        }
        
        public void OnFirstSpawn() {
            SetupParticle();
        }
        public void OnObjectSpawn() {

        }
        public void OnRespawn() {

        }
    }
}