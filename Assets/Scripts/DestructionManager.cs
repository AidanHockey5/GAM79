using UnityEngine;
using System.Collections;

[Singleton]
public class DestructionManager : MonoBehaviour 
{
	public GameObject foundation = null;
	public GameObject explosionEffect = null;
	public int buildingHeightThreshold = 15;

	void Awake()
	{
		InstanceManager.Register (this);
	}

	void Start () 
	{

	}
	

	void Update () 
	{
		
	}

	public void DestroyObject(GameObject building, Vector3 explosionOrigin)
	{
		Instantiate (explosionEffect, explosionOrigin, building.transform.rotation);
		if (building.GetComponent<Rigidbody> () != null) 
		{
			building.GetComponent<Rigidbody> ().AddExplosionForce (building.GetComponent<Rigidbody> ().mass * 100, explosionOrigin, 50000, 2.0f);
		} 
		else 
		{
			GameObject go = Instantiate (foundation, building.transform.position, building.transform.rotation) as GameObject;
			Destroy (building);
		}
//		if (go.transform.childCount != 0) 
//		{
//			Rigidbody []rbs = (Rigidbody[])go.GetComponentsInChildren<Rigidbody> ();
//			foreach (Rigidbody r in rbs) 
//			{
//				r.AddExplosionForce (r.mass * 1000, explosionOrigin/*go.transform.root.position*/, 50000, 2.0f);
//			}	
//		}
	}
}
