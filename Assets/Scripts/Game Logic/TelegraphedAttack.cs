using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TelegraphedAttack : MonoBehaviour
{
    List<GameObject> buildings, players;
    private BaseWeaponController monster;

	// Use this for initialization
	void Start ()
    {
        buildings = new List<GameObject>();
        players = new List<GameObject>();
        monster = GameObject.FindGameObjectWithTag("Monster").GetComponent<BaseWeaponController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject b in buildings)
            {
                b.GetComponent<BreakSwap>().BreakingTime();
            }
            buildings.Clear();

            // Player Damage
            foreach (GameObject p in players)
            {
                Health targetHealth = p.GetComponent<Health>();
                targetHealth.TakeDamage(monster.power);
            }
        }
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<BreakSwap>() != null)
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
        if (col.gameObject.GetComponent<BreakSwap>() != null)
        {
            buildings.Remove(col.gameObject);
        }

        if (col.gameObject.GetComponent<BaseCharacterController>() != null)
        {
            players.Remove(col.gameObject);
        }
    }
}
