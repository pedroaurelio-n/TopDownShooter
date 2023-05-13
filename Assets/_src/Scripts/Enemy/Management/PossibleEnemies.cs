using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class PossibleEnemies : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private List<Enemy> prefabList;

        private int _totalSpawnWeight;

        private void Awake()
        {
            CalculateTotalSpawnWeigth();
        }

        private void CalculateTotalSpawnWeigth()
        {
            _totalSpawnWeight = 0;

            foreach (Enemy enemy in prefabList)
                _totalSpawnWeight += enemy.SpawnWeight;
        }

        public Enemy GetRandomWeightedEnemy()
        {
            var rand = Random.Range(0, _totalSpawnWeight + 1);
            var weightSum = 0;

            foreach (Enemy enemy in prefabList)
            {
                weightSum += enemy.SpawnWeight;
                if (rand <= weightSum)
                    return enemy;
            }

            return null;
        }
    }
}