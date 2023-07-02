using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PedroAurelio.EventSystem;
 
namespace PedroAurelio.TopDownShooter
{
    [RequireComponent(typeof(BoxCollider2DGrid))]
    [RequireComponent(typeof(PossibleEnemies))]
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private Transform target;

        [Header("Settings")]
        [SerializeField] private int maxEnemiesOnScreen;
        [SerializeField] private Vector2 spawnCount;
        [SerializeField] private float startDelay;
        [SerializeField] private float spawnTime;

        [Header("Game Events")]
        [SerializeField] private IntEvent incrementWaveEvent;

        private int _currentWave = 0;

        private PossibleEnemies _possibleEnemies;
        private List<SpawnCell> _activeSpawnCells = new List<SpawnCell>();

        private Coroutine _spawnCoroutine;
        private WaitForSeconds _waitForInterval;

        private void Awake()
        {
            _possibleEnemies = GetComponent<PossibleEnemies>();
        }
        
        private void Start()
        {
            UpdateCurrentWave(0);
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

                SpawnNextWave();

                yield return _waitForInterval;
            }
        }

        private void SpawnNextWave()
        {
            var enemiesToSpawn = Random.Range((int)spawnCount.x, (int)spawnCount.y + 1);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var randomCell = _activeSpawnCells[Random.Range(0, _activeSpawnCells.Count)];
                var randomEnemy = _possibleEnemies.GetRandomWeightedEnemy();
                
                var enemy = Instantiate(randomEnemy, randomCell.RandomPositionInsideBounds(), Quaternion.identity, LevelDependencies.Dynamic);
                enemy.Target = target;
            }
            UpdateCurrentWave();
        }

        private void UpdateCurrentWave(int number = 1)
        {
            _currentWave += number;
            incrementWaveEvent?.RaiseEvent(_currentWave);
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