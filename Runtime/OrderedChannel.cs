using System.Collections.Generic;
using System.Linq;

public class OrderedChannel<T> : IChannel<T> where T : IMessage
{
    public struct OrderedChannelListItem
    {
        public Room<T> Room;
        public int Order;
    }
    
    private List<OrderedChannelListItem> _orderedChannelListItems = new();

    public bool TryAddListener(Room<T> room)
    {
        return TryAddOrderedListener(room);
    }

    public bool TryRemoveListener(Room<T> room)
    {
        _orderedChannelListItems.RemoveAll(item => item.Room == room);
        return true;
    }

    public void Send(T message)
    {
        foreach (var item in _orderedChannelListItems) item.Room.Invoke(message);
    }

    public void Dispose()
    {
        _orderedChannelListItems.Clear();
    }

    public bool TryAddOrderedListener(Room<T> room, int order = 0)
    {
        _orderedChannelListItems.Add(new OrderedChannelListItem
        {
            Room = room,
            Order = order
        });
        _orderedChannelListItems = _orderedChannelListItems.OrderBy(item => item.Order).ToList();
        return true;
    }
}