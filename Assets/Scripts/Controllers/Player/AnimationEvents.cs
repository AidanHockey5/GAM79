using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour 
{
	TelegraphedAttack ta;

	public void TimeOfAttack(int attackAreaIndex)
	{
        ta = this.gameObject.GetComponent<MonsterAbilityManager>().GetAttackArea(attackAreaIndex).GetComponent<TelegraphedAttack>();
        if (ta != null)
        {
			foreach (var building in ta.hitBuildings)
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
                    player.GetComponent<PlayerObject>().RequestTakeDamage(GameEvent.HIT_FROM_HUMAN, ta.damage);
                }
            }
            ta.hitPlayers.Clear();
        }

	}
}
