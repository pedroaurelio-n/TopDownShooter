using UnityEngine;
 
namespace PedroAurelio.Utils
{
    public class VectorUtils
    {
        public static Vector3 InvertVectorX(Vector3 vector)
        {
            var newVector = vector;
            newVector.x *= -1f;
            return newVector;
        }
    }
}