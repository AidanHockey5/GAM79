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
				_instance = new DestructionManager ();
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
		Destroy (cleanVer);
		Instantiate (brokenVer, cleanVer.transform.position, cleanVer.transform.rotation);
		brokenVer.GetComponentInChildren<Rigidbody> ().AddExplosionForce (destructionForce, direction, destructionRadius);
	}

}
