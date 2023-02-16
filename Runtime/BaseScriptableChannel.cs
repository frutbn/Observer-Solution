using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScriptableChannel<T> : ScriptableObject, IChannel<T> where T : IMessage
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
}