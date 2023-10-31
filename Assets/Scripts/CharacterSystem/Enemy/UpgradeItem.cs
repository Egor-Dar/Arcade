using System;
using UnityEngine;

namespace CharacterSystem.Enemy
{
    [Serializable]
    public class UpgrageItem
    {
        [field: SerializeField] public int Second { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float Size { get; private set; }
    }
}