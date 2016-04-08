using UnityEngine;
using System.Collections;

public class AnimationEvents : MonoBehaviour 
{
	public TelegraphedAttack ta;

	public void TimeOfAttack()
	{
		InstanceManager.GetInstance<DestructionManager> ().DestroyBuilding (ta.TA_class_buildings, transform.forward);
		foreach (var player in ta.players) 
		{
			player.GetComponent<PlayerObject>().TakeDamage(GameEvent.HIT_FROM_HUMAN, 1);
		}
		ta.players.Clear();

	}
}
