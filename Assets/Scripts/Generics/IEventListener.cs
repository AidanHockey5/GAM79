using System;

public interface IEventListener
{
    void Subscribe(object subscriber, EventHandler<EventArgs> handler);
    void UnSubscribe(object subscriber, EventHandler<EventArgs> handler);
}
