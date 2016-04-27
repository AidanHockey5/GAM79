using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
	public bool isDead = false;
	public float deathDelay = 3.0f;
	public BuildingTypes buildingType;
	public Animator buildingAnimator = null;
	[SerializeField] private int health = 0;

	private Objective _objective;

	void Awake()
	{
		foreach (BuildingTypes building in BuildingTypes.GetValues(typeof(BuildingTypes))) 
		{
			if (building == buildingType)
			{
				health = (int) building;
			}
		}

		if (GetComponent<Objective> () != null)
		{
			_objective = GetComponent<Objective> ();
		}
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
	}

	void Start ()
    {
		buildingAnimator = this.transform.parent.GetComponent<Animator> ();
	}

	void Update ()
    {
		if (health <= 0) 
		{
			if (_objective != null)
			{
				ObjectiveManager.Instance.CompleteObjective (_objective);
			}

			DestroyBuilding ();
		}
	}

	public void DestroyBuilding()
	{
		Component.Destroy (this.GetComponent<BoxCollider> ());
		gameObject.AddComponent<Rigidbody> ();
		Destroy (this.gameObject, deathDelay);
	}





}
