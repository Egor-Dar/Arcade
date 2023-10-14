using DG.Tweening;
using UnityEngine;

namespace UI.PlayCanvas.Game
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private CharacterSystem.Enemy.Container enemyContainer;
        [SerializeField] private CharacterSystem.Player.Container playerContainer;
        [SerializeField] private ResourceSlider playerHealthBar;
        [SerializeField] private ResourceSlider enemyHealthBar;
        [SerializeField] private ResourceSlider playerManaBar;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            enemyContainer.HealView.OnHealthChangeEvent += enemyHealthBar.SetValue;
            enemyContainer.HealView.OnHealthChangeEvent += (a, b) => enemyHealthBar.FlashingCallback();
            playerContainer.HealView.OnHealthChangeEvent += playerHealthBar.SetValue;
            playerContainer.HealView.OnHealthChangeEvent += (a, b) => playerHealthBar.FlashingCallback();
            playerContainer.ManaView.OnManaChangeEvent += playerManaBar.SetValue;
            playerContainer.ManaView.OnManaChangeEvent +=  (a, b) => playerManaBar.FlashingCallback();
        }

        void IHidenable.Hide()
        {
            canvasGroup.DOFade(0, 0.2f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        void IHidenable.Show()
        {
            canvasGroup.DOFade(1, 0.2f);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }
}