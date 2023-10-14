using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayCanvas
{
    public class ResourceSlider : MonoBehaviour
    {
        [SerializeField] [NotNull] private Slider resourceSlider;
        [SerializeField] private Image image;
        private Color _normalColor;

        private void Awake()
        {
            _normalColor = image.color;
        }

        public void SetValue(float value, float maxValue)
        {
            if (resourceSlider == null) return;
            resourceSlider.maxValue = maxValue;
            resourceSlider.DOValue(value, 0.2f);
        }

        public void FlashingCallback()
        {
            image.DOColor(Color.white, 0.1f).OnComplete((() => image.DOColor(_normalColor, 0.1f)));
        }
    }
}