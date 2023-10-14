using UnityEngine;

namespace CharacterSystem.Enemy
{
    public class FollowerBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private MovementBehaviour movementBehaviour;
        private bool _isStop;
        public void UpdateStop(bool value) => _isStop = value;
        private void FixedUpdate()
        {
            if(_isStop)return;
            var direction = target.position - transform.position;
            movementBehaviour.Move(direction);
        }
    }
}