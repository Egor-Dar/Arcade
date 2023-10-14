using UnityEngine;

namespace CharacterSystem
{
    public class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float speed;

        public void Move(Vector2 direction)
        {
            Vector3 newPosition = direction.normalized * Time.fixedDeltaTime * speed;
            rigidbody2D.MovePosition(transform.position + newPosition);
        }
    }
}