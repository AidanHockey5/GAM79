using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

	public int health = 1000;

	public void TakeDamage(int amount)
	{
		health -= amount;

		if (health <= 0)
		{
			// GameManager.RequestEndGame();
		}
	}
}
