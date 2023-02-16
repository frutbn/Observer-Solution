public interface IChannel<T> where T : IMessage
{
    public bool TryAddListener(Room<T> room);
    public bool TryRemoveListener(Room<T> room);
    public void Send(T message);
}