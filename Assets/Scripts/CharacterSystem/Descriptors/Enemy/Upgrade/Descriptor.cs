using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem.Descriptors.Enemy.Upgrade
{
    [CreateAssetMenu(menuName = "Enemy Descriptor Update")]
    public class Descriptor : ScriptableObject
    {
        [field: SerializeField] public List<Item> Items { get; private set; }
    }
}