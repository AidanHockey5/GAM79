using UnityEngine;
using System.Collections;


public class SpawnPoints : MonoBehaviour 
{
    MonsterDistance monstDistance;
    SpawnPointManager spawnManager;

    public GameObject player;

    
    public float monsterDistance = 0.0f;
    public float spawnCounter = 0.0f;

    public bool isHitting = true;
	// Use this for initialization
	void Start () 
    {
        InstanceManager.GetInstance<SpawnPointManager>().RegisterSpawnPoint(this);
        spawnManager = InstanceManager.GetInstance<SpawnPointManager>();
	}
   
	// Update is called once per frame
    void Update()
    {
        if (spawnManager.gameObject.active == false)
        {
            isHitting = false;
            if (isHitting == false)
            {
                spawnManager.PlayerRebirth(player);
            }
        }
    
    } 
}
