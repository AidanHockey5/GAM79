using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelegraphedAttack : MonoBehaviour
{
    public List<GameObject> hitBuildings, hitPlayers;
    public bool DOT;
    public int damage = 100;
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
			col.gameObject.GetComponent<Building> ().TakeDamage (100);
			if (col.gameObject.GetComponent<Rigidbody> () != null) 
			{
				col.gameObject.GetComponent<Rigidbody> ().AddTorque (Vector3.forward * 1500f);
				col.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.forward * 1500f);
			}
			hitBuildings.Add (col.gameObject);
		}

		if (col.gameObject.GetComponent<PlayerObject> () != null) 
		{
			hitPlayers.Add (col.gameObject);
		}
    }

    void OnTriggerExit(Collider col)
    {
		
    }

    void OnTriggerStay(Collider col)
    {
        if (DOT)
        {
            if (col.gameObject.GetComponent<PlayerObject>() != null)
            {
                timeCounter = timeCounter + Time.deltaTime;
                if (timeCounter > DOTtimer)
                {
					col.gameObject.GetComponent<PlayerObject>().RequestTakeDamage(GameEvent.HIT_FROM_MONSTER, damage);
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
