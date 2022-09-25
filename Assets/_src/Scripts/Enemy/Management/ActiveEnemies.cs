using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public class ActiveEnemies : MonoBehaviour
    {
        private List<Enemy> _enemies = new List<Enemy>();

        private void AddActiveEnemy(Enemy enemy)
        {
            if (!_enemies.Contains(enemy))
                _enemies.Add(enemy);
        }

        private void RemoveActiveEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
                _enemies.Remove(enemy);
        }

        public void DeactivateEnemies()
        {
            foreach (Enemy enemy in _enemies)
                enemy.DisableMovement();
        }

        private void OnEnable()
        {
            Enemy.onEnemySpawned += AddActiveEnemy;
            Enemy.onEnemyDefeated += RemoveActiveEnemy;
        }

        private void OnDisable()
        {
            Enemy.onEnemySpawned -= AddActiveEnemy;
            Enemy.onEnemyDefeated -= RemoveActiveEnemy;
        }
    }
}