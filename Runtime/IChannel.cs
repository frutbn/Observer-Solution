using System;

public interface IChannel<T> : IDisposable where T : IMessage
{
    public bool TryAddListener(Room<T> room);
    public bool TryRemoveListener(Room<T> room);
    public void Send(T message);
}