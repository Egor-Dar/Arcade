using Pool;
using UnityEngine;

namespace Fillers.Element
{
    public interface IFillable : IPoolable<Object>
    {
        Type Type { get; }
        void SetType(Type type);
        void SetCount(float count);
        void SetSprite(Sprite value);
    }
}