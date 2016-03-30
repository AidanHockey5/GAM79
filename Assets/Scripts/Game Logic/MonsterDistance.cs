using UnityEngine;
using System.Collections;

[Singleton]
public class MonsterDistance : MonoBehaviour 
{
    
	// Use this for initialization
	void Start () 
    {
        InstanceManager.Register(this);

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
