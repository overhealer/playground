using System;
using UnityEngine;

namespace playground
{
    public class PoolItem :
        MonoBehaviour
    {
        public Action OnReturnToPool;

        public virtual void ToPool()
        {
            gameObject.SetActive(false);
            OnReturnToPool?.Invoke();
        }

        public virtual void FromPool(Vector3 position)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = position;
        }
    }
}
