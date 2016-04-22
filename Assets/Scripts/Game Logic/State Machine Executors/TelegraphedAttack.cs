using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelegraphedAttack : MonoBehaviour
{
    public List<GameObject> TA_class_buildings, players;
    public bool DOT;
    public int damage;
    public float DOTtimer;
    float timeCounter;
	DestructionManager instance = null;

	// Use this for initialization
	void Start ()
    {
		instance = InstanceManager.GetInstance<DestructionManager> ();
        TA_class_buildings = new List<GameObject>();
        players = new List<GameObject>();
        gameObject.SetActive(false);
        timeCounter = 0;
	}
		
    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.GetComponent<Building>() != null) 
		{
			TA_class_buildings.Add (col.gameObject);
		}

		if (col.gameObject.GetComponent<PlayerObject> () != null) 
		{
			players.Add (col.gameObject);
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
                    col.gameObject.GetComponent<PlayerObject>().RequestTakeDamage(GameEvent.HIT_FROM_HUMAN, damage);
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
