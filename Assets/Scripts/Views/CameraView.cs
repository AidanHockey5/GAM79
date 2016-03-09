using UnityEngine;
using System.Collections;
using thelab.mvc;

public class CameraView : View<KApplication>
{
	Transform transform;

	void Awake()
	{
		transform = gameObject.GetComponent<Transform>();
	}
}

