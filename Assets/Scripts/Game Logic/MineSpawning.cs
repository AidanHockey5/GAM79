using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(NetworkTransform))]
public class MineSpawning : NetworkBehaviour 
{
    public GameObject minePrefab;
    public Transform spawnPoint;

    // Use this for initialization
    [Client]
    void Start()
    {
        if (minePrefab == null)
        {
            minePrefab = (GameObject)(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Mine.fbx", typeof(GameObject)));
           
        }
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {

            CmdSpawnMine(minePrefab, GetComponent<MineSpawner>().transform);
        }
    }
    [Command]
    private void CmdSpawnMine(GameObject minePrefab, Transform spawnPoint)
    {
        GameObject mine = (GameObject)Instantiate(minePrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.Spawn(mine);
    }
}
