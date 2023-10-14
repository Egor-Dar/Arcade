using System.Collections.Generic;
using UnityEngine;

namespace Fillers
{
    [CreateAssetMenu(menuName = "FillersDescriptor")]
    public class Descriptor : ScriptableObject
    {
        [SerializeField] private List<Data> fillers;

        public Dictionary<Element.Type, (float, float, Sprite)> GetFillers()
        {
            if (fillers.Count == 0) return null;
            var dictionary = new Dictionary<Element.Type, (float, float, Sprite)>();
            foreach (var filler in fillers)
            {
                dictionary.Add(filler.FillerType, (filler.SpawnDelay, filler.Count, filler.Sprite));
            }

            return dictionary;
        }
    }
}