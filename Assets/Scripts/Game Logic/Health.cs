using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	
	public int max;
	public int current;

	void Start()
	{
		current = max;
	}

	public void TakeDamage(int amount)
	{
		current -= amount;
	}
}
