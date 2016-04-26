using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class MonsterAbilityController : NetworkBehaviour, IEventListener
{

    #region Private

    [Header("Attack Area Settings")]
    [SerializeField]
    [Tooltip("Index Order Matters!")]
    private GameObject[] attackAreas;
    [SerializeField]
    [Tooltip("Index Order Matters!")]
    private MonsterAbilitySettings[] monsterAbility;
    float timeCounter;

    #endregion

    #region MonoBehaviours

    private void Start()
    {
        for (int i = 0; i < attackAreas.Length; i++)
        {
            attackAreas[i].GetComponent<AttackAreaController>().monsterAbilitySettings = monsterAbility[i];
        }
    }

    #endregion


    #region IEventListener

    public void Subscribe()
	{
		if (isLocalPlayer)
		{
			PlayerObject playerObj = null;
			playerObj = GetComponent<PlayerObject>();

			if (playerObj != null)
			{
				playerObj.RegisterHandler(ReceiveBroadcast);
                monsterAbility = playerObj.monsterAbilitySettings;
			}
		}
	}

	public void UnSubscribe()
	{
		if (isLocalPlayer)
		{
			PlayerObject playerObj = null;
			playerObj = GetComponent<PlayerObject>();

			if (playerObj != null)
			{
				playerObj.UnRegisterHandler(ReceiveBroadcast);
                monsterAbility = null;
			}
		}
	}

	public void ReceiveBroadcast(object sender, GameEventArgs gameEventArgs)
	{
		switch (gameEventArgs.eventType)
		{
		case GameEvent.CHARACTER_FIRE1:
			{

			}
			break;
		case GameEvent.CHARACTER_FIRE2:
			{

			}
			break;
		case GameEvent.CHARACTER_FIRE3:
			{

			}
			break;
		}
	}

    #endregion

    #region Animation Event Functions

    public void AttackAreaOn(int attackAreaIndex)
    {
        attackAreas[attackAreaIndex].SetActive(true);
    }

    public void AttackAreaOff(int attackAreaIndex)
    {
        attackAreas[attackAreaIndex].SetActive(false);
    }

    public void TimeOfAttack(int attackAreaIndex)
    {
        AttackAreaController attackArea = attackAreas[attackAreaIndex].GetComponent<AttackAreaController>();

        if (attackArea != null)
        {
            foreach (var building in attackArea.hitBuildings)
            {
                    building.GetComponent<Building>().TakeDamage(monsterAbility[attackAreaIndex].damage);
            }

            attackArea.hitBuildings.Clear();

            foreach (var player in attackArea.hitPlayers)
            {
                player.GetComponent<PlayerObject>().RequestTakeDamage(GameEvent.HIT_FROM_HUMAN, monsterAbility[attackAreaIndex].damage);
            }

            attackArea.hitPlayers.Clear();
        }
    }

    #endregion

}