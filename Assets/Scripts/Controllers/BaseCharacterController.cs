// Lawrence Lopez

using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkTransform))]
[RequireComponent(typeof(Rigidbody))]
public class BaseCharacterController : NetworkBehaviour 
{
	//Audio
    public AudioClip[] footsteps = null;
    public AudioMixerGroup footstepsMix = null;
    
    [Header("Character Movement")]
	[SerializeField] float inputDelay = 0.1f;
	[SerializeField] float runSpeed = 12;
	[SerializeField] float maxVelocityChange = 12;
	[SerializeField] float jumpHeight = 10.0f;

	[Header("Special Movement")]
	[SerializeField] bool hasJetPack = false;
	[SerializeField] float jetPackCooldown = 5.0f;
	[SerializeField] float jetPackHeight = 100.0f;
	[SerializeField] bool hasGrapplingHook = true;
	[SerializeField] float hookCooldown = 1.0f;
	[SerializeField] float hookDistance = 1000.0f;

	[Header("Camera Movement")]
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
	private bool specialMovementInput;

	// camera movement
	private float yRotation, xRotation;
	private float currentYRotation, currentXRotation;
	private float yRotationVel, xRotationVel;

	private bool canJetPack = true;
	private bool canHook = true;
	private bool hooked = false;
	private Vector3 hookPoint;

//    public int health = 0;
//    public GameObject textBox = null;

    public Quaternion TargetRotation
	{
		get { return targetRotation;}
	}

	void Start () 
	{
		// Because the scene changes from LobbyScene to MainScene when joining/creating a game
		// this field gets nulled at runtime and generates an error.
//        textBox = GameObject.FindGameObjectWithTag("text");
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

		if (Input.GetMouseButton(0))
		{
			CursorOnOff.ChangeCursorState(false);
		}

		if (Input.GetKeyDown (KeyCode.LeftShift))
		{
			SpecialMove();
		}

		if(Input.GetKeyDown(KeyCode.Space) && hooked)
		{
			Jump ();
			hooked = false;
			rBody.useGravity = true;
		}
	}

	[ClientCallback]
	void Update () 
	{
		if (!isLocalPlayer)
			return;
		
		GetInput ();
		Look();

		// GetInput was here twice. The UpdateHealth() function is moved to GameManager.cs
//        GetInput();
//        UpdateHealth();
    }

	[ClientCallback]
	void FixedUpdate()
	{
		if (!hasAuthority)
			return;

		if (hooked)
			ReelIn ();
		else
			Run();
	}

	void Run()
	{
		if (Mathf.Abs(verticalAxisInput) > inputDelay || Mathf.Abs(horizontalAxisInput) > inputDelay)
		{
			Vector3 targetVel = new Vector3(horizontalAxisInput, 0, verticalAxisInput);
			targetVel = transform.TransformDirection(targetVel);
			targetVel.Normalize();
			targetVel *= runSpeed;

			Vector3 velocity = rBody.velocity;
			Vector3 velocityChange = targetVel - velocity;
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rBody.AddForce(velocityChange, ForceMode.VelocityChange);
            AudioManager.audManInst.PlayRandomSfx(footstepsMix, footsteps[Random.Range(0, footsteps.Length)], transform.position);
			// Play run animation
		}
		else
		{
			// zero velocity
			//rBody.velocity = Vector3.zero;
		}
	}

	void Jump()
	{
		rBody.AddForce(new Vector3(0.0f, jumpHeight, 0.0f), ForceMode.Impulse);
	}

	void ReelIn()
	{
		rBody.useGravity = false;
		float smooth = 1.0f;      
		this.transform.position = Vector3.Lerp(this.transform.position, hookPoint, Time.deltaTime * smooth);
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

	void SpecialMove()
	{
		if (hasJetPack && canJetPack)
		{
			rBody.AddForce(new Vector3(0.0f, jetPackHeight, 0.0f), ForceMode.Impulse);
			canJetPack = false;
			StartCoroutine (MovementCooldown (jetPackCooldown));
		}
		else if (hasGrapplingHook && canHook)
		{
			RaycastHit gPoint;
			if (Physics.Raycast(transform.position, playerCam.transform.forward, out gPoint, hookDistance))
			{
				hooked = true;
				hookPoint = gPoint.point;      
				canHook = false;
				StartCoroutine (MovementCooldown (hookCooldown));
			}
		}
	}

	IEnumerator MovementCooldown(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);

		canJetPack = true;
		canHook = true;
	}

	// moved to GameManager.cs
//    public void UpdateHealth()
//    {
//        health = this.GetComponent<Health>().GetHealth();
//        textBox.GetComponent<Text>().text = "Health:" + health.ToString();
//    }
}
