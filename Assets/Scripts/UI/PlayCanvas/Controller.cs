using System.Collections;
using CharacterSystem;
using UnityEngine;

namespace UI.PlayCanvas
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Game.Controller gameScreen;
        [SerializeField] private Lose.Controller loseScreen;
        [SerializeField] private Win.Controller winScreen;
        [SerializeField] private CharacterSystem.Player.Container player;
        [SerializeField] private CharacterSystem.Enemy.Container enemy;
        [SerializeField] private StopObserver stopObserver;
        [SerializeField] private ReplayBehaviour replayBehaviour;
        [SerializeField] private Timer timer;
        private CanvasRechanger _canvasRechanger;
        private bool _isFinish;

        private void Awake()
        {
            _canvasRechanger = new CanvasRechanger();
            replayBehaviour.ReplayEvent += SetGameScreen;
            SetGameScreen();
        }

        private void Start()
        {
            player.HealView.OnDeathEvent += SetLoseScreen;
            enemy.HealView.OnDeathEvent += SetWinScreen;
        }

        private void SetGameScreen()
        {
            _isFinish = false;
            StartCoroutine(StopDelay());
            _canvasRechanger.SetNewPanel(gameScreen);
            timer.StartNewTimer();
        }

        private IEnumerator StopDelay()
        {
            yield return new WaitForSeconds(1f);
            stopObserver.OnStopCallback(false);
        }

        private void SetLoseScreen()
        {
            if (_isFinish) return;
            _isFinish = true;
            stopObserver.OnStopCallback(true);
            _canvasRechanger.SetNewPanel(loseScreen);
            timer.PauseTimer();
        }

        private void SetWinScreen()
        {
            if (_isFinish) return;
            _isFinish = true;
            stopObserver.OnStopCallback(true);
            _canvasRechanger.SetNewPanel(winScreen);
        }
    }
}