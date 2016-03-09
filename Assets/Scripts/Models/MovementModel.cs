using UnityEngine;
using System.Collections;
using thelab.mvc;

public class MovementModel : Model<KApplication>
{
	public float acceleration;
	public float maxVelocityChange;
}
