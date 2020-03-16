using UnityEngine;
using UniRx.Toolkit;

namespace Azarashi.Utilities
{
    public class TransformObjectPool : ObjectPool<Transform>
    {
        GameObject prefab;

        public TransformObjectPool(GameObject prefab)
        {
            this.prefab = prefab;
        }

        protected override Transform CreateInstance()
        {
            return Object.Instantiate(prefab).transform;
        }
    }
}