using System;
using UnityEngine;

namespace CharacterSystem.Stats
{
    public class ManaStatsBehaviour : MonoBehaviour, IReplenishmentView
    {
        public float MaxMana { get; private set; }
        public float CurrentMana { get; private set; }
        public event Action<float, float> OnManaChangeEvent;
        public event Action OnWeakenedEvent;

        public void SpendMana(float value)
        {
            CurrentMana = Mathf.Clamp(CurrentMana - value, 0, MaxMana);
            OnManaChangeEvent?.Invoke(CurrentMana, MaxMana);
        }

        public void ReplenishMana(float value)
        {
            CurrentMana = Mathf.Clamp(CurrentMana + value, 0, MaxMana);
            OnManaChangeEvent?.Invoke(CurrentMana, MaxMana);
        }

        public void SetMaxMana(float value)
        {
            MaxMana = value;
            CurrentMana = MaxMana;
            OnManaChangeEvent?.Invoke(CurrentMana, MaxMana);
        }
    }
}