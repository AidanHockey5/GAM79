using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour
{
	public string buildingType = string.Empty;
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
		
	}

	void Update ()
    {
		if (health >= 0) 
		{
			//animator
		}
	}




}
