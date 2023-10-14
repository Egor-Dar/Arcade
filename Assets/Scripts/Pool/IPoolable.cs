using System;
using UnityEngine;

namespace Pool
{
    public interface IPoolable<T> where T: MonoBehaviour
    {
        event Action<T> ReturnInPoolEvent;
        void Init();
        void SetPosition(Vector3 position);
        void SetVisible(bool value);
    }
}