using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System;

public class MineSpawning : NetworkBehaviour, IEventListener
{
    public GameObject minePrefab;
    public Transform spawnPoint;
    public float spawnRate = 1.0f;

    private int mineCounter;
    private bool canSpawn = true;

    #region Monobehaviours

    [ClientCallback]
    void Start()
    {
        if (minePrefab == null)
        {
            //minePrefab = (GameObject)(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Mine.fbx", typeof(GameObject))); 
        }

        mineCounter = 0;

        if (NetworkClient.active)
        {
            Subscribe();
        }
    }

    void OnDestroy()
    {
        UnSubscribe();
    }

    #endregion

    [Command]
    private void CmdSpawnMine()
    {
        if (mineCounter >= 0 && canSpawn == true)
        {
            GameObject mine = (GameObject) Instantiate(minePrefab, spawnPoint.position, spawnPoint.rotation);
            NetworkServer.Spawn(mine);
            mineCounter++;
            canSpawn = false;
            StartCoroutine(StartSpawnTimer(spawnRate));
        }
        else if (mineCounter >= 3)
        {
            mineCounter = 0;
        }
        
    }

    private IEnumerator StartSpawnTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canSpawn = true;
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
                    // gameEventArgs.eventArgs[0] - bool fire2Input
                    if ((bool) gameEventArgs.eventArgs[0])
                    {
                        CmdSpawnMine();
                    }        
                }
                break;
        }
    }

    #endregion

}
