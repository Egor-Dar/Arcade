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
        [SerializeField] private Descriptors.Player.Descriptor descriptor;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private ManaStatsBehaviour manaStatsBehaviour;
        [SerializeField] private StopObserver stopObserver;
        [SerializeField] private JoystickListener joystickListener;
        [SerializeField] private MovementBehaviour movementBehaviour;

        [FormerlySerializedAs("bulletSpawner")] [SerializeField]
        private Initializer initializer;

        [SerializeField] private ReplayBehaviour replayBehaviour;
        private Vector3 _startPosition;
        public IHealView HealView { get; private set; }
        public IReplenishmentView ManaView { get; private set; }
        public bool IsEnemy => false;
        private float _coefDamage;

        private void Awake()
        {
            _coefDamage = descriptor.StartedCoeficientDamage;
            _startPosition = transform.position;
            HealView = healthStatsBehaviour;
            ManaView = manaStatsBehaviour;
            healthStatsBehaviour.SetMaxHealth(descriptor.MaxHealth);
            manaStatsBehaviour.SetMaxMana(descriptor.MaxMana);
            manaStatsBehaviour.SpendMana(descriptor.MaxMana);
            stopObserver.Subscribe(joystickListener.UpdateStop);
            stopObserver.Subscribe(initializer.UpdateStop);
            replayBehaviour.ReplayEvent += Replay;
            ManaView.OnManaChangeEvent += CheckManaAndShotKrit;
            movementBehaviour.SetMovementSpeed(descriptor.MovementSpeed);
        }

        private void Replay()
        {
            transform.DOMove(_startPosition, 0.2f);
            healthStatsBehaviour.SetMaxHealth(descriptor.MaxHealth);
            manaStatsBehaviour.SpendMana(descriptor.MaxMana);
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
                _coefDamage += descriptor.UpdateAddedCoeficientDamage;
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