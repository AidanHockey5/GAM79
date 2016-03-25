using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour 
{
	public TelegraphedAttack ta;

	public void AttackKeyframeTrigger()
	{
		foreach (GameObject b in ta.buildings)
		{
			b.GetComponent<BreakSwap>().BreakingTime();
		}
		ta.buildings.Clear();

		// Player Damage
		foreach (GameObject p in ta.players)
		{
			Health targetHealth = p.GetComponent<Health>();
			// targetHealth.TakeDamage(monster.power);
		}
	}
}
