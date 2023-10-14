using System;
using CharacterSystem.Player;
using UnityEngine;

namespace Fillers.Element
{
    public class Object : MonoBehaviour, IFillable
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Collider2D collider;
        private float _count;
        private bool _isActive;
        public event Action<Object> ReturnInPoolEvent;
        public Type Type { get; private set; }

        public void Init()
        {
            _isActive = true;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetVisible(bool value)
        {
            spriteRenderer.enabled = value;
            collider.enabled = value;
        }

        public void SetType(Type type)
        {
            Type = type;
        }

        public void SetCount(float count)
        {
            _count = count;
        }

        public void SetSprite(Sprite value)
        {
            spriteRenderer.sprite = value;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isActive == false) return;
            if (other.TryGetComponent(out IStatsReplenishable statsReplisherable))
            {
                statsReplisherable.Replenish(Type, _count);
                ReturnInPoolEvent?.Invoke(this);
            }
        }
    }
}