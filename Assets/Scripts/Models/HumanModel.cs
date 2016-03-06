using UnityEngine;
using System.Collections;
using thelab.mvc;

public class HumanModel : KModel
{
	public float runSpeed;
	public float maxVelocityChange;
	public float lookSensitivity;
	public float lookSmoothDamp;
	public float xLowerCamBound;
	public float xUpperCamBound;

	public Rigidbody rBody;
	public float verticalAxisInput, horizontalAxisInput, mouseXInput, mouseYInput;

	// camera movement
	public float yRotation, xRotation;
	public float currentYRotation, currentXRotation;
	public float yRotationVel, xRotationVel;
	public bool isCursorVisible;
}
