using System;

namespace CharacterSystem.Stats
{
    public interface IReplenishmentView
    {
        public float MaxMana { get; }
        public float CurrentMana { get; }
        public event Action<float, float> OnManaChangeEvent;
        public event Action OnWeakenedEvent;
        public void SpendMana(float value);
        public void ReplenishMana(float value);
    }
}