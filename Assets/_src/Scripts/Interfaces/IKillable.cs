using UnityEngine;

namespace TopDownShooter
{
    public interface IKillable
    {
        public void Damage();
        public void Death();
    }
}