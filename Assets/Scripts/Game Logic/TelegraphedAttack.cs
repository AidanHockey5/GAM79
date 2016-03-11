using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelegraphedAttack : MonoBehaviour
{
    List<GameObject> buildings, players;

	// Use this for initialization
	void Start ()
    {
        buildings = new List<GameObject>();
        players = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject b in buildings)
            {
//                b.GetComponent<BreakSwap>().BreakingTime();
				b.GetComponent<Building>().PrepareDestruction (b.transform.position);
            }

            buildings.Clear();
        }
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Building>() != null)
        {
            buildings.Add(col.gameObject);
        }

        if (col.gameObject.GetComponent<BaseCharacterController>() != null)
        {
            players.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
		if (col.gameObject.GetComponent<Building>() != null)
        {
            buildings.Remove(col.gameObject);
        }

        if (col.gameObject.GetComponent<BaseCharacterController>() != null)
        {
            players.Remove(col.gameObject);
        }
    }
}
