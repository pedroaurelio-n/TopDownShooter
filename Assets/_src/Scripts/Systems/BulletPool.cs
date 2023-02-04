using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace TopDownShooter
{
    public class BulletPool : ObjectPool<Bullet>
    {
        public BulletPool(Bullet obj) : base(obj)
        {
        }

        private Bullet CreateObject(BulletSO bulletSO)
        {
            var newObject = Object.Instantiate(_object);
            newObject.BulletSO = bulletSO;
            Pool.Add(newObject);
            return newObject;
        }

        public Bullet GetObject(BulletSO bulletSO)
        {
            Bullet bullet;

            for (int i = 0; i < Pool.Count; i++)
            {
                if (Pool[i].BulletSO != bulletSO)
                    continue;
                    
                if (!Pool[i].gameObject.activeInHierarchy)
                {
                    bullet = Pool[i];
                    bullet.gameObject.SetActive(true);
                    bullet.Initialize();
                    return bullet;
                }
            }

            bullet = CreateObject(bulletSO);
            bullet.gameObject.SetActive(true);
            bullet.Initialize();
            return bullet;
        }
    }
}