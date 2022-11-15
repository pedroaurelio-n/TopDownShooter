using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    [RequireComponent(typeof(BoxCollider2DGrid))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Vector2 spawnCount;
        [SerializeField] private float startDelay;
        [SerializeField] private float spawnTime;

        private List<SpawnCell> _activeSpawnCells = new List<SpawnCell>();

        private Coroutine _spawnCoroutine;
        private WaitForSeconds _waitForInterval;
        
        private void Start()
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
            _waitForInterval = new WaitForSeconds(spawnTime);
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(startDelay);

            while (true)
            {
                var enemiesToSpawn = Random.Range((int)spawnCount.x, (int)spawnCount.y + 1);
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    var randomCell = _activeSpawnCells[Random.Range(0, _activeSpawnCells.Count)];
                    var enemy = Instantiate(enemyPrefab, randomCell.RandomPositionInsideBounds(), Quaternion.identity, LevelDependencies.Dynamic);
                    enemy.Target = target;
                    Debug.Log(enemy.Target.name);
                }

                yield return _waitForInterval;
            }
        }

        public void StopSpawnCoroutine() => StopCoroutine(_spawnCoroutine);

        public void DisableActiveEnemies()
        {
            foreach (Enemy enemy in Enemy.EnemyInstances)
                enemy.Disable();
        }

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

        private void OnDisable()
        {
            Enemy.EnemyInstances.Clear();
        }
    }
}