using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BaseCameraController : MonoBehaviour 
{
	public Transform target;
	public float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0,6,-8);
	public float xTilt = 10;

	Vector3 destination = Vector3.zero;
	BaseCharacterController charController;
	float rotateVel = 0;

	void Start () 
	{
		SetCameraTarget(target);
	}

	void SetCameraTarget(Transform t)
	{
		target = t;

		if (target != null)
		{
			if (target.GetComponent<BaseCharacterController>())
			{
				charController = target.GetComponent<BaseCharacterController>();
			}
			else
			{
				Debug.LogError("The camera's target needs a character controller.");
			}
		}
		else
		{
			Debug.LogError("Your camera needs a target.");
		}

	}
	
	void LateUpdate () 
	{
		//moving
		MoveToTarget();
		//rotating
		LookAtTarget();
	}

	void MoveToTarget()
	{
		destination = charController.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget()
	{
		float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler(xTilt, eulerYAngle, 0);
	}

}
