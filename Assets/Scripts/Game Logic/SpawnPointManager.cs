using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    SpawnPoints spawn;
    MonsterDistance monstDist;
 
    public List<SpawnPoints> spawnPointObject = new List<SpawnPoints>();

    public float monstDistance = 0.0f;
    public float pointDistance;

    public bool isReady = false;
    void Awake()
    {
        InstanceManager.Register(this);
    }
    // Use this for initialization
   
	void Start () 
    {
        spawn = GetComponent<SpawnPoints>();
        monstDist = GetComponent<MonsterDistance>();

        
	}
    void Update()
    {
       
    }

    public void RegisterSpawnPoints(SpawnPoints item)
    {
        
         spawnPointObject.Add(item);
        
    }

    public void PlayerRebirth(GameObject player)
    {
        player.transform.position = spawn.transform.position;
    }

   public  void PointLocation(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        int i = 0 ;

       while( i < hitColliders.Length)
       {
           pointDistance = Vector3.Distance(spawn.transform.position, monstDist.transform.position); 
       }
       if (pointDistance <= 150f && pointDistance >= 300f)
       {
           isReady = true;
       }
       if (isReady == true)
       {
           PlayerRebirth(player);
       }
    }

   public GameObject player { get; set; }
}
