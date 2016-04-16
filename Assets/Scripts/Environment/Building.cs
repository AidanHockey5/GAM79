using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
	public bool isDead = false;
	public float deathDelay = 3.0f;
	public string buildingType = string.Empty;
	public Animator buildingAnimator = null;
	[SerializeField] private int health = 0;

	void Awake()
	{
		foreach (buildingTypes building in buildingTypes.GetValues(typeof(buildingTypes))) 
		{
			if (building.ToString() == buildingType)
			{
				health = (int) building;
			}
		}
	}

	public void TakeDamage()
	{
		health--;
	}

	void Start ()
    {
		buildingAnimator = this.transform.parent.GetComponent<Animator> ();
	}

	void Update ()
    {
		if (health >= 0) 
		{
			buildingAnimator.SetBool ("isDead", true);
			Destroy (this, deathDelay);
		}
	}




}
