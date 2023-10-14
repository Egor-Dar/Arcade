using CharacterSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayCanvas.Win
{
    public class Controller : MonoBehaviour, IHidenable
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button replayButton;
        [SerializeField] private ReplayBehaviour replayBehaviour;

        private void Awake()
        {
            replayButton.onClick.AddListener(replayBehaviour.ReplayCallback);
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