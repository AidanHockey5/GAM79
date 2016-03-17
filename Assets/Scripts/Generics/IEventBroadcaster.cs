using System;

public interface IEventBroadcaster
{
    event EventHandler<EventArgs> m_handler;
    void RegisterHandler(EventHandler<EventArgs> p_handler);
    void UnRegisterHandler(EventHandler<EventArgs> p_handler);
}
