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
	[SerializeField] float hookDistance = 50.0f;
	[SerializeField] float hookReelSpeed = 15.0f;

	[Header("Camera Movement")]
	[SerializeField] float lookSensitivity = 5f;
	[SerializeField] float lookSmoothDamp = 0.1f;
	[SerializeField] int xLowerCameraBound = -75;
	[SerializeField] int xUpperCameraBound = 90;
    [SerializeField] CameraSettings cameraPos;
	[SerializeField] Camera playerCam;
    [SerializeField] Transform cameraTar;

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

    public bool isRunning = false;

    public Quaternion TargetRotation
	{
		get { return targetRotation;}
	}

	void Start () 
	{
        CursorOnOff.ChangeCursorState(false);
        if (!isLocalPlayer)
        {
            playerCam.enabled = false;
        }		
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
        isRunning = true;

	}

	void GetInput()
	{
        if (Input.GetMouseButton(0))
        {
            CursorOnOff.ChangeCursorState(false);
            isRunning = true;
        }

        if (!isRunning)
            return;

        verticalAxisInput = Input.GetAxis("Vertical");
		horizontalAxisInput = Input.GetAxis("Horizontal");
		mouseXInput = Input.GetAxis("Mouse X");
		mouseYInput = Input.GetAxis("Mouse Y");

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CursorOnOff.ChangeCursorState(true);
            isRunning = false;
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
            /*
            void CmdMove(float hInput, float vInput)
            {
                tarDir = new Vector3(hInput, 0, vInput);
                tarDir = transform.TransformDirection(tarDir).normalized;
                tarDir *= movementSettings.accelerationRate;
                tarDir.z = Mathf.Clamp(tarDir.z, -movementSettings.maxSpeed, movementSettings.maxSpeed);
                tarDir.y += movementSettings.gravity * Time.deltaTime;
                charController.Move(tarDir * Time.deltaTime);
            }
            */

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
            // AudioManager.audManInst.PlayRandomSfx(footstepsMix, footsteps[Random.Range(0, footsteps.Length)], transform.position);
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
		this.transform.position = Vector3.MoveTowards(this.transform.position, hookPoint, Time.deltaTime * hookReelSpeed);
	}

	void Look()
	{
		yRotation += mouseXInput * lookSensitivity;
		xRotation += mouseYInput * lookSensitivity;
		xRotation = Mathf.Clamp(xRotation, xLowerCameraBound, xUpperCameraBound);
		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVel, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVel, lookSmoothDamp);
		transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        targetRotation = transform.rotation;
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
}
