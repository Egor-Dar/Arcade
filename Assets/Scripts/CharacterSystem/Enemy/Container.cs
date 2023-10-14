using CharacterSystem.Stats;
using DG.Tweening;
using UnityEngine;

namespace CharacterSystem.Enemy
{
    public class Container : MonoBehaviour, IDamagable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float damage;
        [SerializeField] private HealthStatsBehaviour healthStatsBehaviour;
        [SerializeField] private TriggerDamage triggerDamage;
        [SerializeField] private FollowerBehaviour followerBehaviour;
        [SerializeField] private StopObserver stopObserver;
        [SerializeField] private ReplayBehaviour replayBehaviour;
        private Vector3 _startPosition;
        public IHealView HealView { get; private set; }
        public bool IsEnemy => true;

        private void Awake()
        {
            _startPosition = transform.position;
            HealView = healthStatsBehaviour;
            healthStatsBehaviour.SetMaxHealth(maxHealth);
            triggerDamage.SetDamage(damage);
            triggerDamage.DetectEnemy(false);
            stopObserver.Subscribe(followerBehaviour.UpdateStop);
            replayBehaviour.ReplayEvent += Replay;
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