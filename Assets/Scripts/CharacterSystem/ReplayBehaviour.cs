using System;
using UnityEngine;

namespace CharacterSystem
{
    public class ReplayBehaviour : MonoBehaviour
    {
        public event Action ReplayEvent;
        public void ReplayCallback()
        {
            ReplayEvent?.Invoke();
        }
    }
}