using UnityEngine;
using System.Collections;

public class MonsterAttackDamage : MonoBehaviour 
{
	public int power;

	private TelegraphedAttack monsterAttack;

	// Use this for initialization
	void Start () 
	{
		monsterAttack = GetComponent<TelegraphedAttack> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void Damage()
	{
		foreach (GameObject p in monsterAttack.players) 
		{
			Health health = p.GetComponent<Health> ();
			health.GetComponent<Health>().TakeDamage(power);
		}
	}
}
