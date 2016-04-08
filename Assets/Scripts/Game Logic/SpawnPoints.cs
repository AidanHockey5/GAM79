using UnityEngine;
using System.Collections;


public class SpawnPoints : MonoBehaviour 
{
    
	// Use this for initialization
	void Start () 
    {
		SpawnPointManager.Instance.RegisterSpawnPoint (this);
        
	}
   
	// Update is called once per frame
    void Update()
    {
           
    } 
}
