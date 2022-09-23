using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public class WeaponDrop : Collectable
    {
        [SerializeField] private List<ShootingPattern> patterns;

        private ShootingPattern _newPattern;

        private void Awake()
        {
            var randomPattern = Random.Range(0, patterns.Count);
            _newPattern = patterns[randomPattern];
        }

        protected override void CollectAction(Player player)
        {
            player.Shoot.ChangePattern(_newPattern);
        }
    }
}