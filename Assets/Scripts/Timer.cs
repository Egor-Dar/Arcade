using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Coroutine _coroutine;
    public event Action<int> UpdateEvent;
    private int _second;

    private void Awake()
    {
        _coroutine = StartCoroutine(TimeLoop());
    }

    public void StartNewTimer()
    {
        _second = 0;
        _coroutine = StartCoroutine(TimeLoop());
    }

    public void PauseTimer()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
    }

    public void RestartTimer()
    {
        _coroutine = StartCoroutine(TimeLoop());
    }

    private void OnDestroy()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
    }

    private IEnumerator TimeLoop()
    {
        for (;;)
        {
            yield return new WaitForSeconds(1f);
            _second++;
            UpdateEvent?.Invoke(_second);
        }
    }
}