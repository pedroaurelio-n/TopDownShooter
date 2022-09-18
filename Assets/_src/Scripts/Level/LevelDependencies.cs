using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public class LevelDependencies : MonoBehaviour
    {
        public static Player Player { get; private set; }
        public static Transform Dynamic { get; private set; }

        [SerializeField] private Player player;
        [SerializeField] private Transform dynamic;

        private void Awake()
        {
            Player = player;
            Dynamic = dynamic;
        }
    }
}