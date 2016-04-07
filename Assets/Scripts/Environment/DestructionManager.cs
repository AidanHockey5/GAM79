using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class DestructionManager : MonoBehaviour 
{
	public float hitForce = 1500.0f;

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

	public void DestroyBuilding(List<GameObject> buildings, Vector3 dirVector)
	{
		foreach (GameObject building in buildings) 
		{
			if (building.transform.childCount == 0) 
			{
				if (building.GetComponent<Rigidbody> () != null) 
				{
					building.GetComponent<Rigidbody> ().AddTorque (dirVector * hitForce);
					building.GetComponent<Rigidbody> ().AddForce (dirVector * hitForce);
				} 
				else 
				{
					Component.Destroy (building.GetComponent<BoxCollider> ());
					building.AddComponent<Rigidbody> ();
					Destroy (building, 10f);
				}
			}
		}

		buildings.Clear ();
	}

}
