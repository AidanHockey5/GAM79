// Lawrence Lopez
// https://www.youtube.com/watch?v=BBS2nIKzmbw (Thank you Renaissance Coders)

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class BaseCharacterController : NetworkBehaviour 
{
	public float inputDelay = 0.1f;
	public float forwardVel = 12;
	public float rotateVel = 100;

	Quaternion targetRotation;
	Rigidbody rBody;
	float forwardInput, turnInput;

	public GameObject playerCamera;

	public Quaternion TargetRotation
	{
		get { return targetRotation;}
	}

	void Start () 
	{
		if (!isLocalPlayer)
			playerCamera.SetActive (false);
		
		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody> ()) 
		{
			rBody = GetComponent<Rigidbody> ();
		} 
		else 
		{
			Debug.LogError ("This component requires a Rigidbody.");
		}

		forwardInput = turnInput = 0;
	}

	void GetInput()
	{
		forwardInput = Input.GetAxis("Vertical");
		turnInput = Input.GetAxis("Horizontal");
	}

	[ClientCallback]
	void Update () 
	{
		if (!isLocalPlayer)
			return;
		
		GetInput ();
		Turn ();
	}

	[ClientCallback]
	void FixedUpdate()
	{
		if (!hasAuthority)
			return;

		Run();
	}

	void Run()
	{
		if (Mathf.Abs(forwardInput) > inputDelay)
		{
			// move
			rBody.velocity = transform.forward * forwardInput * forwardVel;
		}
		else
		{
			// zero velocity
			rBody.velocity = Vector3.zero;
		}
	}

	void Turn()
	{
		if (Mathf.Abs(turnInput) > inputDelay)
		{
			targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
		}
			transform.rotation = targetRotation;
	}

}
