using System;

public class GameEventArgs : EventArgs
{
    public GameEvent eventType { get; set; }
    public object[] eventArgs { get; set; }
}
