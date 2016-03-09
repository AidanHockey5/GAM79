using UnityEngine;
using System.Collections;
using thelab.mvc;

public class InputModel : Model<KApplication>
{
	public float inputDelay = 0.01f;
	public float verticalAxisInput, horizontalAxisInput, mouseXInput, mouseYInput;
}

