using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelegraphedAttack : MonoBehaviour
{
    public List<GameObject> buildings, players;
	DestructionManager instance = null;

	// Use this for initialization
	void Start ()
    {
		instance = InstanceManager.GetInstance<DestructionManager> ();
        buildings = new List<GameObject>();
        players = new List<GameObject>();
        gameObject.SetActive(false);
	}



    void OnTriggerEnter(Collider col)
    {
		print (col.name);
		instance.buildings.Add (col.gameObject);
//        if (col.gameObject.GetComponent<BreakSwap>() != null)
//        {
//            buildings.Add(col.gameObject);
//        }
//
//        if (col.gameObject.GetComponent<BaseCharacterController>() != null)
//        {
//            players.Add(col.gameObject);
//        }
    }

    void OnTriggerExit(Collider col)
    {
//        if (col.gameObject.GetComponent<BreakSwap>() != null)
//        {
//            buildings.Remove(col.gameObject);
//        }
//
//        if (col.gameObject.GetComponent<BaseCharacterController>() != null)
//        {
//            players.Remove(col.gameObject);
//        }
    }
}
