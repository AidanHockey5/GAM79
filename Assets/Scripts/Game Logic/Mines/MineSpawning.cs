using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class MineSpawning : NetworkBehaviour, IEventListener
{
    public GameObject minePrefab;
    private Transform spawnPoint;

    public int mineCounter;

    #region Monobehaviours

    void Start()
    {
        mineCounter = 0;

		ClientScene.RegisterPrefab (minePrefab);

		if(isLocalPlayer)
			Subscribe ();
    }

    #endregion

    [Command]
    private void CmdSpawnMine()
    {
		spawnPoint = this.transform;
		GameObject mine = (GameObject)Instantiate(minePrefab, spawnPoint.position - new Vector3(0.0f, 0.5f, 0.0f), spawnPoint.rotation);
        NetworkServer.Spawn(mine);
    }

    #region IEventListener

    public void Subscribe()
    {
        InputManager.Instance.RegisterHandler(ReceiveBroadcast);
    }

    public void UnSubscribe()
    {
        InputManager.Instance.UnRegisterHandler(ReceiveBroadcast);
    }

    public void ReceiveBroadcast(object sender, GameEventArgs gameEventArgs)
    {
        switch (gameEventArgs.eventType)
        {
            case GameEvent.CHARACTER_FIRE2:
                {
                    if (mineCounter >= 0)
                    {
                        CmdSpawnMine();
                        mineCounter++;
                    }

                    if (mineCounter == 3)
                    {
                        mineCounter = 0;
                    }

                    if (mineCounter == 0)
                    {
                        return;// function for reload or a pick up function would go here
                    }
                }
                break;
        }
    }

    #endregion

}
