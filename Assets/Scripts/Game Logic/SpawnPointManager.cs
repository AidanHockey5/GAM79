using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
   

    public List<SpawnPoints> spawnPointObject = new List<SpawnPoints>();

   

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

    public void PlayerRebirth(GameObject player)
    {

    }
}
