using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAreaController : MonoBehaviour 
{
	public List<GameObject> hitBuildings, hitPlayers;
	public bool DOT;
	public int damage;
	public float DOTtimer;
	float timeCounter;

	// Use this for initialization
	void Start ()
	{
		hitBuildings = new List<GameObject>();
		hitPlayers = new List<GameObject>();
		gameObject.SetActive(false);
		timeCounter = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.GetComponent<Building>() != null) 
		{
			hitBuildings.Add (col.gameObject);
		}

		if (col.gameObject.GetComponent<PlayerObject> () != null) 
		{
			hitPlayers.Add (col.gameObject);
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (DOT)
		{
			PlayerObject po = col.gameObject.GetComponent<PlayerObject> ();

			if (po)
			{
				timeCounter = timeCounter + Time.deltaTime;
				if (timeCounter > DOTtimer)
				{
					po.RequestTakeDamage(GameEvent.HIT_FROM_MONSTER, damage);
					timeCounter = 0;
				}
			}
		}
	}

	void OnEnable()
	{
		timeCounter = 0;
	}

}
