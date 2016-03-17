using UnityEngine;
using System.Collections;

public class Health : IMessageHandler
{
    private PlayerSettings player;

    void Start()
    {
        
    }

    void HandleDamage(params object[] args)
    {
        
    }

    // IMessageHanlder implemenation
    public void HandleMessage(GameEvent eventType, params object[] args)
    {
       // if (eventType == player.playerType)
       // HandleDamage(args);
    }
    
}
