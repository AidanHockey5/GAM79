using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour 
{
	TelegraphedAttack ta;
	public MonsterAbilityManager monsterAbilityMan = null;

	public void TimeOfAttack(int attackAreaIndex)
	{
		if (!monsterAbilityMan)
		{
			return;
		}
		else
		{
			ta = monsterAbilityMan.attackAreaScripts[attackAreaIndex];

			if (ta != null)
			{
				foreach (GameObject building in ta.hitBuildings)
				{
					if (ta.DOT)
					{

					}
					else
					{
						building.GetComponent<Building> ().TakeDamage (ta.damage);
					}
				}
				ta.hitBuildings.Clear ();
				foreach (var player in ta.hitPlayers)
				{
					if (ta.DOT)
					{
						//Do Damage over time code
					}
					else
					{
						player.GetComponent<PlayerObject>().RequestTakeDamage(GameEvent.HIT_FROM_MONSTER, ta.damage);
					}
				}
				ta.hitPlayers.Clear();
			}
		}
	}
}
