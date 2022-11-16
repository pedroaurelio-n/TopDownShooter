using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public abstract class Collectable : MonoBehaviour
    {
        [field: SerializeField, Range(0f, 1f)] public float DropChance { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                CollectAction(player);
                Destroy(gameObject);
            }
        }

        protected abstract void CollectAction(Player player);
    }
}