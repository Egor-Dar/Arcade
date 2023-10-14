using System;

namespace CharacterSystem.Stats
{
    public interface IHealView
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; }
        public event Action<float, float> OnHealthChangeEvent;
        public event Action OnDeathEvent;
        public void SpendHealth(float value);
        public void ReplenishHealth(float value);
    }
}