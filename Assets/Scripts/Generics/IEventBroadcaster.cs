using System;

public interface IEventBroadcaster
{
    event EventHandler<GameEventArgs> EventHandlerMainDelegate;
    void RegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate);
    void UnRegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate);
    void BroadcastEvent(GameEvent type, params object[] args);
}
