using System;

public interface IEventListener
{
    void Subscribe();
    void UnSubscribe();
    void ReceiveBroadcast(object sender, GameEventArgs gameEventArgs);
}
