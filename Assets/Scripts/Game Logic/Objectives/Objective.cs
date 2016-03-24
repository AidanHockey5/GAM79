using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour
{
	public string title = "Objective Title";
	public string description = "Objective Description";
	public int objectivePriority = 0;
	public Vector3 location = Vector3.zero;

	private bool _complete;

	protected virtual void Awake()
	{
		location = gameObject.transform.position;
		ObjectiveManager.Instance.RegisterObjectve (this);
	}
}
