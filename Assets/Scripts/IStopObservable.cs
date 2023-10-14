using System;

public interface IStopObservable
{
    void OnStopCallback(bool value);
    void Subscribe(Action<bool> value);
    void Unsubscribe(Action<bool> value);
}