using System;

public interface IEventListener
{
    void Subscribe(object subscriber, EventHandler<GameEventArgs> p_handler);
    void UnSubscribe(object subscriber, EventHandler<GameEventArgs> p_handler);
}
