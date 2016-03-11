using UnityEngine;
using System.Collections;

public class DestructionManager : MonoBehaviour 
{
	public float destructionForce = 0.0f;
	public float destructionRadius = 0.0f;

	private static DestructionManager _instance;

	public static DestructionManager instance
	{
		get
		{ 
			if (_instance == null) 
			{
				GameObject go = new GameObject ();
				_instance = go.AddComponent<DestructionManager> ();
			}

			return _instance;
		}
	}

	void Start () 
	{

	}
	

	void Update () 
	{
	
	}

	public void DestroyObject(GameObject cleanVer, GameObject brokenVer, Vector3 direction)
	{
		GameObject go = Instantiate (brokenVer, cleanVer.transform.position, cleanVer.transform.rotation) as GameObject;
		Debug.Log (go);
//		rb = go.GetComponent<Rigidbody> ();
//		childRB = go.GetComponentsInChildren<Rigidbody> (); 

		Destroy (cleanVer);
//		rb.AddExplosionForce (destructionForce, direction, destructionRadius);
//		foreach (Rigidbody r in childRB)
//		{
//			r.AddExplosionForce (destructionForce, direction, destructionRadius);
//		}

		if (go.transform.childCount != 0) 
		{
			Rigidbody []rbs = (Rigidbody[])go.GetComponentsInChildren<Rigidbody> ();
			foreach (Rigidbody r in rbs) 
			{
				Debug.Log (r.gameObject.name);
				//r.AddExplosionForce (destructionForce, go.transform.root.position, destructionRadius, 2.0f);
				r.AddExplosionForce (r.mass * 1000, go.transform.root.position, 50000, 2.0f);
				//r.velocity = Vector3.up * 40;
			}	
		} 
		else 
		{
			Rigidbody r = go.GetComponent<Rigidbody> ();
			if(r!=null)
				r.AddExplosionForce (destructionForce, direction, destructionRadius);
		}
	}

}
