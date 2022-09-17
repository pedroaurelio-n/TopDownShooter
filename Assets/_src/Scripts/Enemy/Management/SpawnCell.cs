using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public class SpawnCell : MonoBehaviour
    {
        private EnemySpawner _spawner;

        private void Start()
        {
            _spawner = transform.parent.GetComponent<EnemySpawner>();
            _spawner.AddActiveSpawnCell(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out Player player))
                _spawner.RemoveActiveSpawnCell(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<Player>(out Player player))
                _spawner.AddActiveSpawnCell(this);
        }
    }
}