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

	void Awake()
	{
		foreach (BuildingTypes building in BuildingTypes.GetValues(typeof(BuildingTypes))) 
		{
			if (building == buildingType)
			{
				health = (int) building;
			}
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
		if (health >= 0) 
		{
//			buildingAnimator.SetBool ("isDead", true);
			Destroy (this.gameObject, deathDelay);
		}
	}




}
