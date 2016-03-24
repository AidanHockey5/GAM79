using System;

public interface IEventBroadcaster
{
    event EventHandler<GameEventArgs> m_handler;
    void RegisterHandler(EventHandler<GameEventArgs> p_handler);
    void UnRegisterHandler(EventHandler<GameEventArgs> p_handler);
    void BroadcastEvent(GameEvent eventType, params object[] args);
}
