using System;
using Unity.VisualScripting;
using UnityEngine;

namespace CharacterSystem.Descriptors.Enemy.Upgrade
{
    [Serializable]
    public class Item
    {
        [field: SerializeField] public int Second { get; private set; }
        [field: SerializeField] public ContainerValue Damage { get; private set; }
        [field: SerializeField] public ContainerValue Speed { get; private set; }
        [field: SerializeField] public ContainerValue Size { get; private set; }
        [field: SerializeField] public ContainerValue Shoot { get; private set; }

        [Serializable]
        public class ContainerValue
        {
            [field: SerializeField] public bool Actual { get; private set; }
            [field: SerializeField] public float Value { get; private set; }
        }
    }
}