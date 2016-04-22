using UnityEngine;
using System.Collections;

public class ProjectileMotor : MonoBehaviour
{
	public float speed;
	public float lifetime;

	new Transform transform;

	void Start () 
	{
		transform = GetComponent<Transform> ();
		Destroy(gameObject, lifetime);
	}
	
	void Update () 
	{
		ApplyThrust ();
	}

	void ApplyThrust()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
