using UnityEngine;
using System.Collections;
using thelab.mvc;

public class GunView : View <KApplication>
{
	public Transform transform;

	void Awake()
	{
		transform = gameObject.GetComponent<Transform>();
	}
}
