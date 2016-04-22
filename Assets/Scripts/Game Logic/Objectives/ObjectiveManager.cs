using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour 
{
	private List<Objective> _objectives = new List<Objective>();
	private static ObjectiveManager _instance;

	public static ObjectiveManager Instance
	{
		get { return _instance; }
		set { _instance = value; }
	}

	void Awake()
	{
		_instance = this;
	}

	public void RegisterObjectve(Objective o)
	{
		if (!_objectives.Contains (o))
			_objectives.Add (o);

		SortObjectives ();
	}

	private void SortObjectives()
	{
		_objectives.Sort ((a, b) => a.objectivePriority.CompareTo(b.objectivePriority));
	}

	private Objective CurrentObjective()
	{
		return _objectives [0];
	}

	public void CompleteCurrentObjective()
	{
		_objectives.RemoveAt(0);
	}

	public void CompleteObjective(Objective o)
	{
		if (o.isMainObjective)
			GameManager.Instance.MainObjectiveComplete ();
		_objectives.Remove (o);
	}

	public Vector3 CurrentObjectiveLocation()
	{
		return CurrentObjective ().location;
	}

	public string CurrentObjectiveTitle()
	{
		return CurrentObjective ().title;
	}

	public string CurrentObjectiveDescription()
	{
		return CurrentObjective ().description;
	}


}
