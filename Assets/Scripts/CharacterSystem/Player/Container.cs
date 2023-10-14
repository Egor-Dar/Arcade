using Bullet;
using CharacterSystem.Stats;
using DG.Tweening;
using Fillers.Element;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterSystem.Player
{
    public class Container : MonoBehaviour, IDamagable, IStatsReplenishable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private ManaStatsBehaviour manaStatsBehaviour;
        [SerializeField] private StopObserver stopObserver;
        [SerializeField] private JoystickListener joystickListener;
        [FormerlySerializedAs("bulletSpawner")] [SerializeField] private Initializer initializer;
        [SerializeField] private ReplayBehaviour replayBehaviour;
        private Vector3 _startPosition;
        public IHealView HealView { get; private set; }
        public IReplenishmentView ManaView { get; private set; }
        public bool IsEnemy => false;
        private int _coefDamage;

        private void Awake()
        {
            _coefDamage = 2;
            _startPosition = transform.position;
            HealView = healthStatsBehaviour;
            ManaView = manaStatsBehaviour;
            healthStatsBehaviour.SetMaxHealth(maxHealth);
            manaStatsBehaviour.SetMaxMana(1);
            manaStatsBehaviour.SpendMana(1);
            stopObserver.Subscribe(joystickListener.UpdateStop);
            stopObserver.Subscribe(initializer.UpdateStop);
            replayBehaviour.ReplayEvent += Replay;
            ManaView.OnManaChangeEvent += CheckManaAndShotKrit;
        }

        private void Replay()
        {
            transform.DOMove(_startPosition, 0.2f);
            healthStatsBehaviour.SetMaxHealth(maxHealth);
            manaStatsBehaviour.SpendMana(1);
        }

        private void CheckManaAndShotKrit(float current, float max)
        {
            if (current != max)
            {
                initializer.UpdateDamage(current + _coefDamage * 0.1f);
            }
            else
            {
                initializer.InitKritBullet(_coefDamage);
                _coefDamage += 4;
                manaStatsBehaviour.SpendMana(1);
            }
        }

        private void OnDestroy()
        {
            stopObserver.Unsubscribe(joystickListener.UpdateStop);
            stopObserver.Unsubscribe(initializer.UpdateStop);
        }

        public void TakeDamage(float value)
        {
            healthStatsBehaviour.SpendHealth(value);
        }

        public void TakeHeal(float value)
        {
            healthStatsBehaviour.ReplenishHealth(value);
        }

        public void Replenish(Type type, float value)
        {
            switch (type)
            {
                case Type.Health:
                    HealView.ReplenishHealth(value);
                    break;
                case Type.Mana:
                    ManaView.ReplenishMana(value);
                    break;
            }
        }
    }
}