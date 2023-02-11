using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    [RequireComponent(typeof(BoxCollider2DGrid))]
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform target;
        [SerializeField] private List<Enemy> enemyPrefabs;

        [Header("Settings")]
        [SerializeField] private int maxEnemiesOnScreen;
        [SerializeField] private Vector2 spawnCount;
        [SerializeField] private float startDelay;
        [SerializeField] private float spawnTime;

        private int _totalSpawnWeight;

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
                if (Enemy.EnemyInstances.Count > maxEnemiesOnScreen)
                {
                    yield return _waitForInterval;
                    continue;
                }

                var enemiesToSpawn = Random.Range((int)spawnCount.x, (int)spawnCount.y + 1);
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    var randomCell = _activeSpawnCells[Random.Range(0, _activeSpawnCells.Count)];
                    var enemy = Instantiate(GetRandomWeightedEnemy(), randomCell.RandomPositionInsideBounds(), Quaternion.identity, LevelDependencies.Dynamic);
                    enemy.Target = target;
                }

                yield return _waitForInterval;
            }
        }

        private Enemy GetRandomWeightedEnemy()
        {
            _totalSpawnWeight = 0;

            foreach (Enemy enemy in enemyPrefabs)
                _totalSpawnWeight += enemy.SpawnWeight;

            var rand = Random.Range(0, _totalSpawnWeight + 1);
            var weightSum = 0;

            foreach (Enemy enemy in enemyPrefabs)
            {
                weightSum += enemy.SpawnWeight;
                if (rand <= weightSum)
                    return enemy;
            }

            return null;
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

        private void OnDisable() => Enemy.EnemyInstances.Clear();
    }
}