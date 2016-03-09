using UnityEngine;
using System.Collections;
using thelab.mvc;

public class CameraModel : Model<KApplication>
{
	public float lookSensitivity;
	public float lookSmoothDamp;
	public float xLowerCamBound;
	public float xUpperCamBound;
	public float yRotation, xRotation;
	public float currentYRotation, currentXRotation;
	public float yRotationVel, xRotationVel;
	public bool isCursorVisible;
}

