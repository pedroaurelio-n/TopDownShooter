using UnityEngine;
 
namespace PedroAurelio.Utils
{
    public class BoolUtils
    {
        public static bool CalculateRandomChance(float chance)
        {
            var threshold = 1f - chance;
            var rand = Random.Range(0f, 1f);
            return rand >= threshold ? true : false;
        }
    }
}