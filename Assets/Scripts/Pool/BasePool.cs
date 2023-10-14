using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class BasePool<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
    {
        [SerializeField] protected T prefab;
        [SerializeField] private int countInAwake;
        protected Queue<T> _instances;
        public event Action InitializeFinished;

        private void Awake()
        {
            _instances = new Queue<T>();
            for (int i = 0; i < countInAwake; i++)
            {
                AddNewInstance();
            }
            InitializeFinished?.Invoke();
        }

        protected virtual void AddNewInstance()
        {
            var instance = Instantiate(prefab);
            instance.SetVisible(false);
            instance.ReturnInPoolEvent += ReturnInPool;
            _instances.Enqueue(instance);
        }

        protected virtual void ReturnInPool(T instance)
        {
            instance.SetVisible(false);
            _instances.Enqueue(instance);
        }

        public virtual T GetInstance()
        {
            if (_instances.Count == 0)
            {
                AddNewInstance();
            }
            return _instances.Dequeue();
        }
    }
}