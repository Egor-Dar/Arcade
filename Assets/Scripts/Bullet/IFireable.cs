using Pool;
using UnityEngine;

namespace Bullet
{
    public interface IFireable : IPoolable<Linker>
    {
        void SetDamage(float value);
        void IsCrit(bool value);
        void AddForce(Vector2 direction);
    }
}