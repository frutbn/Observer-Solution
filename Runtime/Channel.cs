using System.Collections.Generic;

public sealed class Channel<T> : IChannel<T> where T : IMessage
{
    private readonly HashSet<Room<T>> _rooms = new();

    public bool TryAddListener(Room<T> room)
    {
        return _rooms.Add(room);
    }

    public bool TryRemoveListener(Room<T> room)
    {
        return _rooms.Remove(room);
    }

    public void Send(T message)
    {
        foreach (var room in _rooms) room.Invoke(message);
    }
    
    public void Dispose()
    {
        _rooms.Clear();
    }
}