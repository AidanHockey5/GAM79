using UnityEngine;
using System.Collections;
using thelab.mvc;

public class MovementView : View<KApplication>
{
	public Rigidbody rigidBody;

	void Start()
	{
		rigidBody = gameObject.GetComponent<Rigidbody>();
	}

}