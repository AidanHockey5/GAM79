using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    Health health;

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
        player.transform.position = new Vector3(0, 0.7f, 0);
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
