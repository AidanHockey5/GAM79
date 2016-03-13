using UnityEngine;
using System.Collections;

public class DestructionManager : MonoBehaviour 
{
	/*public float destructionForce = 0.0f;
	public float destructionRadius = 0.0f;*/

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

	public void DestroyObject(GameObject cleanVer, GameObject brokenVer, Vector3 explosionOrigin)
	{
		GameObject go = Instantiate (brokenVer, cleanVer.transform.position, cleanVer.transform.rotation) as GameObject;
		Destroy (cleanVer);

		if (go.transform.childCount != 0) 
		{
			Rigidbody []rbs = (Rigidbody[])go.GetComponentsInChildren<Rigidbody> ();
			foreach (Rigidbody r in rbs) 
			{
				Debug.Log (r.gameObject.name);
				r.AddExplosionForce (r.mass * 1000, explosionOrigin/*go.transform.root.position*/, 50000, 2.0f);
			}	
		} 
		else 
		{
			Rigidbody r = go.GetComponent<Rigidbody> ();
			if(r!=null)
				r.AddExplosionForce (r.mass * 1000, explosionOrigin, 50000);
		}
	}

}
