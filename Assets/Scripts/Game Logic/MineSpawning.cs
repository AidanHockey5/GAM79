using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(NetworkTransform))]
public class MineSpawning : NetworkBehaviour 
{
    public GameObject minePrefab;
    public Transform spawnPoint;

    public int mineCounter;
    // Use this for initialization
    [Client]
    void Start()
    {
        if (minePrefab == null)
        {
            minePrefab = (GameObject)(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Mine.fbx", typeof(GameObject)));
           
        }

        mineCounter = 0;
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (mineCounter >= 0)
            {
                CmdSpawnMine(minePrefab, GetComponent<MineSpawner>().transform);
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
    }
    [Command]
    private void CmdSpawnMine(GameObject minePrefab, Transform spawnPoint)
    {
        GameObject mine = (GameObject)Instantiate(minePrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.Spawn(mine);
    }
}
