using System.Collections.Generic;
using UnityEngine;
 
namespace PedroAurelio.TopDownShooter
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        protected T _object;
        public List<T> Pool;

        public ObjectPool(T obj)
        {
            _object = obj;
            Pool = new List<T>();
        }

        protected T CreateObject()
        {
            var newObject = Object.Instantiate(_object);
            Pool.Add(newObject);
            return newObject;
        }

        public T GetObject()
        {
            for (int i = 0; i < Pool.Count; i++)
            {
                if (!Pool[i].gameObject.activeInHierarchy)
                {
                    Pool[i].gameObject.SetActive(true);
                    return Pool[i];
                }
            }

            return CreateObject();
        }

        public void ReleaseObject(T obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}