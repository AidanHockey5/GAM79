using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    public List<SpawnPoints> spawnPointObject = new List<SpawnPoints>();

    public Transform player;

    float monstDistance = 0.0f;
    float pointDistance = 0.0f;

    void Awake()
    {
        InstanceManager.Register(this);
    }
    // Use this for initialization
   
	void Start () 
    {
        player = gameObject.GetComponent<BaseCharacterController>().transform;
	}
    void Update()
    {
        foreach (SpawnPoints item in spawnPointObject)
        {
            spawnPointObject.Add(item);

           /* if (item != null)
            {
                
            }*/
        }
    }

    public void RegisterMonsterDistance()
    {
        monstDistance = Vector3.Distance(gameObject.GetComponent<MonsterDistance>().transform.position, player.position);
    }

    public void RegisterSpawnPoints()
    {
        pointDistance = Vector3.Distance((gameObject.GetComponent<SpawnPoints>().transform.position), gameObject.GetComponent<MonsterDistance>().transform.position);
    }

    public void RegisterGameManager()
    {

    }

    public void PlayerRebirth(GameObject player)
    {
        player.transform.position = new Vector3(0, 0, 0);
    }

    public void OnTriggerEnter(Collider other)
    {

    }
}
