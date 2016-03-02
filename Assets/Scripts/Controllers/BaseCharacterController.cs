// Lawrence Lopez
// https://www.youtube.com/watch?v=BBS2nIKzmbw (Thank you Renaissance Coders)

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class BaseCharacterController : NetworkBehaviour 
{
	// character movement
	[SerializeField] float inputDelay = 0.1f;
	[SerializeField] float runSpeed = 12;
	[SerializeField] float maxVelocityChange = 12;

	// camera movement
	[SerializeField] float lookSensitivity = 5f;
	[SerializeField] float lookSmoothDamp = 0.1f;
	[SerializeField] int xLowerCameraBound = -75;
	[SerializeField] int xUpperCameraBound = 90;
	[SerializeField] Camera playerCam;

	// character movement
	private Quaternion targetRotation;
	private Rigidbody rBody;
	private float verticalAxisInput, horizontalAxisInput, mouseXInput, mouseYInput;
	private float forceModifier = 0.01f;

	// camera movement
	private float yRotation, xRotation;
	private float currentYRotation, currentXRotation;
	private float yRotationVel, xRotationVel;

	public Quaternion TargetRotation
	{
		get { return targetRotation;}
	}

	void Start () 
	{
		CursorOnOff.ChangeCursorState(false);

		if (!isLocalPlayer)
			playerCam.enabled = false;
		
		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody> ()) 
		{
			rBody = GetComponent<Rigidbody> ();
		} 
		else 
		{
			Debug.LogError ("This component requires a Rigidbody.");
		}

		verticalAxisInput = horizontalAxisInput = 0;
	}

	void GetInput()
	{
		verticalAxisInput = Input.GetAxis("Vertical");
		horizontalAxisInput = Input.GetAxis("Horizontal");
		mouseXInput = Input.GetAxis("Mouse X");
		mouseYInput = Input.GetAxis("Mouse Y");

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CursorOnOff.ChangeCursorState(true);
		}
	}

	[ClientCallback]
	void Update () 
	{
		if (!isLocalPlayer)
			return;
		
		GetInput ();
		Look();
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
		if (Mathf.Abs(verticalAxisInput) > inputDelay || Mathf.Abs(horizontalAxisInput) > inputDelay)
		{
			Vector3 targetVel = new Vector3(horizontalAxisInput, 0, verticalAxisInput);
			targetVel = transform.TransformDirection(targetVel);
			targetVel *= runSpeed;

			Vector3 velocity = rBody.velocity;
			Vector3 velocityChange = targetVel - velocity;
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rBody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
		else
		{
			// zero velocity
			rBody.velocity = Vector3.zero;
		}
	}

	void Look()
	{
		yRotation += mouseXInput * lookSensitivity;
		xRotation += mouseYInput * lookSensitivity;
		xRotation = Mathf.Clamp(xRotation, xLowerCameraBound, xUpperCameraBound);
		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVel, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVel, lookSmoothDamp);
		transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
		playerCam.transform.localRotation = Quaternion.Euler(-currentXRotation, 0, 0);
	}
}
