using UnityEngine;
using System.Collections;

[Singleton]
public class Test2 : MonoBehaviour 
{
	// Use this for initialization
	void Start () {
		var test = InstanceManager.GetInstance<Test1> ();
		if (test != null)
			Debug.LogError ("Got the thing!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
