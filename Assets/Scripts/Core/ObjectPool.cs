using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace playground
{
    public class ObjectPool<TPoolObject>
    {
        public List<TPoolObject> PooledObjects, ActiveObjects;

        public ObjectPool(TPoolObject[] poolObject, int poolLength, Transform container = null, bool randomPrefabs = true)
        {
            PooledObjects = new List<TPoolObject>(poolLength);
            ActiveObjects = new List<TPoolObject>();

            for (int i = 0; i < poolLength; i++)
            {
                int id = randomPrefabs ? Random.Range(0, poolObject.Length) : i % poolObject.Length;
                PoolItem o = GameObject.Instantiate(poolObject[id] as PoolItem);
                if (container)
                    o.transform.SetParent(container);

                o.gameObject.SetActive(false);
                PooledObjects.Add((TPoolObject)(object)o);
            }
        }

        public ObjectPool(TPoolObject poolObject, int poolLength, Transform container = null)
        {
            PooledObjects = new List<TPoolObject>(poolLength);
            ActiveObjects = new List<TPoolObject>();

            for (int i = 0; i < poolLength; i++)
            {
                PoolItem o = GameObject.Instantiate(poolObject as PoolItem);
                if (container)
                    o.transform.SetParent(container);

                o.gameObject.SetActive(false);
                PooledObjects.Add((TPoolObject)(object)o);
            }
        }

        public TPoolObject TakeFromPool(Vector3 pos)
        {
            TPoolObject poolObject = PooledObjects[0];
            PooledObjects.Remove(poolObject);
            ActiveObjects.Add(poolObject);
            (poolObject as PoolItem).FromPool(pos);
            return poolObject;
        }

        public void ReturnToPool(TPoolObject poolObject)
        {
            ActiveObjects.Remove(poolObject);
            PooledObjects.Add(poolObject);
            (poolObject as PoolItem).ToPool();
        }
    }
}
