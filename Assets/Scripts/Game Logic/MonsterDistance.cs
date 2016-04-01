using UnityEngine;
using System.Collections;

public class MonsterDistance : MonoBehaviour 
{
    void Awake()
    {

     }
	// Use this for initialization
	void Start () 
    {
        InstanceManager.GetInstance<SpawnPointManager>().RegisterMonsterDistance();
        
	}
        
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
