using System;
using CharacterSystem.Enemy;
using UnityEngine;

namespace Bullet
{
    public class Linker : MonoBehaviour, IFireable
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private Visualize visualize;
        [field: SerializeField] public TriggerDamage TriggerDamage { get; private set; }
        public event Action<Linker> ReturnInPoolEvent;

        private void Awake()
        {
            TriggerDamage.TriggerEnteredEvent += () =>
            {
                SetVisible(false);
                rigidbody2D.velocity = Vector2.zero;
                ReturnInPoolEvent?.Invoke(this);
            };
        }

        public void Init()
        {
            SetVisible(true);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetVisible(bool value)
        {
            visualize.gameObject.SetActive(value);
        }

        public void SetDamage(float value)
        {
            TriggerDamage.SetDamage(value);
        }

        public void IsCrit(bool value)
        {
            visualize.SetVisualize(value);
        }

        public void AddForce(Vector2 direction)
        {
            rigidbody2D.AddForce(direction);
        }
    }
}