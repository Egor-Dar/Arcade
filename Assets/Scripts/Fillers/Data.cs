using System;
using UnityEngine;

namespace Fillers
{
    [Serializable]
    public class Data
    {
        [field: SerializeField] public Fillers.Element.Type FillerType { get; private set; }
        [field: SerializeField] public float SpawnDelay { get; private set; }
        [field: SerializeField] public float Count { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}