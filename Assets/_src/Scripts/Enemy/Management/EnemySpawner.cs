using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(BoxCollider2DGrid))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Vector2 spawnCount;
        [SerializeField] private float spawnTime;

        private List<SpawnCell> _activeSpawnCells = new List<SpawnCell>();

        private Coroutine _spawnCoroutine;
        
        private void Start()
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnTime);

                var enemiesToSpawn = Random.Range((int)spawnCount.x, (int)spawnCount.y + 1);
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    var randomCell = _activeSpawnCells[Random.Range(0, _activeSpawnCells.Count)];
                    var enemy = Instantiate(enemyPrefab, randomCell.RandomPositionInsideBounds(), Quaternion.identity, LevelDependencies.Dynamic);
                }
            }
        }

        public void StopSpawnCoroutine() => StopCoroutine(_spawnCoroutine);

        public void AddActiveSpawnCell(SpawnCell cell)
        {
            if (!_activeSpawnCells.Contains(cell))
                _activeSpawnCells.Add(cell);
        }

        public void RemoveActiveSpawnCell(SpawnCell cell)
        {
            if (_activeSpawnCells.Contains(cell))
                _activeSpawnCells.Remove(cell);
        }
    }
}