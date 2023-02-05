using UnityEngine;
using PedroAurelio.EventSystem;
 
namespace PedroAurelio.TopDownShooter
{
    public class HealthDrop : Collectable
    {
        [Header("Health Settings")]
        [SerializeField] private float healthAdd;

        [Header("Score Settings")]
        [SerializeField] private IntEvent collectScoreEvent;
        [SerializeField] private int score;

        protected override void CollectAction(Player player)
        {
            player.Health.IncreaseHealth(healthAdd, out bool reachedMaxHealth);

            if (reachedMaxHealth)
                collectScoreEvent?.RaiseEvent(score);
        }
    }
}