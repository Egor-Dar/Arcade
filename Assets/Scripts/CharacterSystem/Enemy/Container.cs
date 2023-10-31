using System.Collections.Generic;
using CharacterSystem.Descriptors.Enemy.Upgrade;
using CharacterSystem.Stats;
using DG.Tweening;
using UnityEngine;

namespace CharacterSystem.Enemy
{
    public class Container : MonoBehaviour, IDamagable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float damage;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private TriggerDamage triggerDamage;
        [SerializeField] private MovementBehaviour movementBehaviour;
        [SerializeField] private FollowerBehaviour followerBehaviour;
        [SerializeField] private StopObserver stopObserver;
        [SerializeField] private ReplayBehaviour replayBehaviour;
        [SerializeField] private Descriptor upgradeDescriptor;
        [SerializeField] private Bullet.Pool bulletPool;
        [SerializeField] private Timer timer;
        private Vector3 _startPosition;
        private Dictionary<int, Item> _upgrade;
        public IHealView HealView { get; private set; }
        public bool IsEnemy => true;

        private void Awake()
        {
            bulletPool.Initialize(false);
            _upgrade = new Dictionary<int, Item>();
            foreach (var item in upgradeDescriptor.Items)
            {
                _upgrade.Add(item.Second, item);
            }

            timer.UpdateEvent += CheckTimeUpgrade;
            _startPosition = transform.position;
            HealView = healthStatsBehaviour;
            healthStatsBehaviour.SetMaxHealth(maxHealth);
            triggerDamage.SetDamage(damage);
            triggerDamage.DetectEnemy(false);
            stopObserver.Subscribe(followerBehaviour.UpdateStop);
            replayBehaviour.ReplayEvent += Replay;
        }

        private void CheckTimeUpgrade(int value)
        {
            if (_upgrade.TryGetValue(value, out var item))
            {
                if (item.Damage.Actual) triggerDamage.SetDamage(item.Damage.Value);
                if (item.Speed.Actual) movementBehaviour.SetMovementSpeed(item.Speed.Value);
                if (item.Size.Actual) transform.DOScale(item.Size.Value, 0.2f);
                if (item.Shoot.Actual) Shoot((int)item.Shoot.Value);
            }
        }

        private void Shoot(int value)
        {
            float angleStep = 360f / value;

            for (int i = 0; i < value; i++)
            {
                float angle = i * angleStep;
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

                var instance = bulletPool.GetInstance();
                instance.SetDamage(damage);
                instance.SetVisible(true);
                instance.SetPosition(transform.position);
                instance.AddForce(direction * bulletSpeed);
                instance.IsCrit(false);
                instance.Init();
            }
        }

        private void Replay()
        {
            transform.DOMove(_startPosition, 0.2f);
            healthStatsBehaviour.SetMaxHealth(maxHealth);
        }

        private void OnDestroy()
        {
            stopObserver.Unsubscribe(followerBehaviour.UpdateStop);
        }

        public void TakeDamage(float value)
        {
            healthStatsBehaviour.SpendHealth(value);
        }

        public void TakeHeal(float value)
        {
            healthStatsBehaviour.ReplenishHealth(value);
        }
    }
}