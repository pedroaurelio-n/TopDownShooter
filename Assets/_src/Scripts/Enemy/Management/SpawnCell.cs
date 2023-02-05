using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class SpawnCell : MonoBehaviour
    {
        [SerializeField] private Vector2 spawnZoneDecrease;
        private EnemySpawner _spawner;
        private BoxCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();

            _spawner = transform.parent.GetComponent<EnemySpawner>();
            _spawner.AddActiveSpawnCell(this);
        }

        public Vector2 RandomPositionInsideBounds()
        {
            var boundsMin = _collider.bounds.min;
            var boundsMax = _collider.bounds.max;

            var position = new Vector2
            (
                Random.Range(boundsMin.x + spawnZoneDecrease.x, boundsMax.x - spawnZoneDecrease.x), 
                Random.Range(boundsMin.y + spawnZoneDecrease.y, boundsMax.y - spawnZoneDecrease.y)
            );

            return position;
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