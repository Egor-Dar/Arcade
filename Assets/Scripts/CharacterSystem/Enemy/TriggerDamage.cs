using System;
using UnityEngine;

namespace CharacterSystem.Enemy
{
    public class TriggerDamage : MonoBehaviour
    {
        private float _damage;
        private bool _isEnemyLayer;
        public event Action TriggerEnteredEvent;

        public void SetDamage(float damage) => _damage = damage;
        public void DetectEnemy(bool isEnemy) => _isEnemyLayer = isEnemy;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
            {
                if (damagable.IsEnemy != _isEnemyLayer) return;
                damagable.TakeDamage(_damage);
            }
            TriggerEnteredEvent?.Invoke();
        }
    }
}