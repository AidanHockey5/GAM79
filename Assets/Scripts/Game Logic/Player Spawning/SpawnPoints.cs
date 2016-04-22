using UnityEngine;
using System.Collections;


public class SpawnPoints : MonoBehaviour 
{
    public int pointNumber;
 
        // Use this for initialization
	void Start () 
    {
		SpawnPointManager.Instance.RegisterSpawnPoint (pointNumber, this);
        
	}
   
	// Update is called once per frame
    void Update()
    {
           
    } 
}
