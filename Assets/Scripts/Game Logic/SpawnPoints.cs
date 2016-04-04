using UnityEngine;
using System.Collections;


public class SpawnPoints : MonoBehaviour 
{
    
	// Use this for initialization
	void Start () 
    {
        InstanceManager.GetInstance<SpawnPointManager>().RegisterSpawnPoints();
        
	}
   
	// Update is called once per frame
    void Update()
    {
           
    } 
}
