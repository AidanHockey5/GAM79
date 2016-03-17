using UnityEngine;
using System.Collections;

public interface IMessageHandler
{
    void HandleMessage(GameEvent eventType, params object[] args);
}
