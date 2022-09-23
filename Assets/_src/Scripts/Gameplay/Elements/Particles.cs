using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
 
namespace TopDownShooter
{
    public class Particles : MonoBehaviour
    {
        private IObjectPool<Particles> _pool;
        private ParticleSystem _ps;

        public void SetPool(IObjectPool<Particles> pool) => _pool = pool;

        public void Initialize(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;

            if (_ps == null)
            {
                _ps = GetComponent<ParticleSystem>();
                var main = _ps.main;
                main.stopAction = ParticleSystemStopAction.Callback;
            }
        }

        private void OnParticleSystemStopped() => _pool.Release(this);
    }
}