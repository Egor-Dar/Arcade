using UnityEngine;

namespace Bullet
{
    public class Visualize : MonoBehaviour
    {
        [SerializeField] private Sprite defaultBullet;
        [SerializeField] private Sprite critical;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetVisualize(bool isCrit)
        {
            if (isCrit)
            {
                spriteRenderer.sprite = critical;
            }
            else
            {
                spriteRenderer.sprite = defaultBullet;
            }
        }
    }
}