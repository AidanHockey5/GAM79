﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelegraphedAttack : MonoBehaviour
{
    public List<GameObject> TA_class_buildings, players;
	DestructionManager instance = null;

	void Start ()
    {
		instance = InstanceManager.GetInstance<DestructionManager> ();
        TA_class_buildings = new List<GameObject>();
        players = new List<GameObject>();
        gameObject.SetActive(false);
	}
		
    void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.transform.childCount == 0) 
		{
			TA_class_buildings.Add (col.gameObject);
		}
		if (col.gameObject.GetComponent<PlayerObject> () != null) 
		{
			players.Add (col.gameObject);
		}
		if (col.gameObject.GetComponent<Building>() != null) 
		{
			col.gameObject.GetComponent<Building> ().TakeDamage ();
		}
    }

    void OnTriggerExit(Collider col)
    {
		
    }
}
