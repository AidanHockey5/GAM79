using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelegraphedAttack : MonoBehaviour
{
    public List<GameObject> TA_class_buildings, players;
	DestructionManager instance = null;

	// Use this for initialization
	void Start ()
    {
		instance = InstanceManager.GetInstance<DestructionManager> ();
        TA_class_buildings = new List<GameObject>();
        players = new List<GameObject>();
        gameObject.SetActive(false);
	}



    void OnTriggerEnter(Collider col)
    {
		print (col.name);
		if (col.gameObject.transform.childCount == 0) 
		{
			TA_class_buildings.Add (col.gameObject);
		}

    }

    void OnTriggerExit(Collider col)
    {
		
    }
}
