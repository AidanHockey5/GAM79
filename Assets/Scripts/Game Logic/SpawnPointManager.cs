using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    BaseCharacterController player;
    MonsterDistance monsterDistance;
    SpawnPoints spPoint;

    public List<SpawnPoints> spawnPointObject = new List<SpawnPoints>();

    public float monstDistance = 0.0f;
    public float pointDistance = 0.0f;

    void Awake()
    {
        InstanceManager.Register(this);
    }
    // Use this for initialization
   
	void Start () 
    {
       
        player = GetComponent<BaseCharacterController>();
        monsterDistance = GetComponent<MonsterDistance>();
        spPoint = GetComponent<SpawnPoints>();

        monstDistance = Vector3.Distance(monsterDistance.transform.position, player.transform.position);
        pointDistance = Vector3.Distance(spPoint.transform.position, monsterDistance.transform.position);

      
       
	}
    void Update()
    {
     
      /*  foreach (SpawnPoints item in spawnPointObject)
        {
            

            if (item != null)
            {
                
            }
        }*/
    }

    public void RegisterSpawnPoints(SpawnPoints item)
    {
        spawnPointObject.Add(item);

        
    }
    
     
    public void RegisterGameManager()
    {

    }

    public void PlayerRebirth(GameObject player)
    {
        player.transform.position = new Vector3(0, 0, 0);
    }

   public  void PointLocation(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

       int i = hitColliders.Length;

        if (i != null)
        {
           
        }
    }
}
