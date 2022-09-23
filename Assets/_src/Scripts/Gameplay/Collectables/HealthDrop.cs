using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public class HealthDrop : Collectable
    {
        [SerializeField] private float healthAdd;

        protected override void CollectAction(Player player) => player.Health.ModifyHealth(healthAdd);
    }
}