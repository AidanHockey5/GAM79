using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class MonsterAbilityController : NetworkBehaviour, IEventListener
{

    #region Public Fields


    #endregion

    #region Private Members

    [Header("Attack Area Settings")]
    [SerializeField]
    [Tooltip("Index Order Matters!")]
    private GameObject[] attackAreas;
    [SerializeField]
    [Tooltip("Index Order Matters!")]
    private MonsterAbilitySettings[] monsterAbilitySettings;
    float timeCounter;
	TelegraphedAttack ta;

    #endregion

    #region MonoBehaviours

    private void Start()
    {
        for (int i = 0; i < attackAreas.Length; i++)
        {
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
                monsterAbilitySettings = playerObj.monsterAbilitySettings;
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
                monsterAbilitySettings = null;
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

    public GameObject GetAttackArea(int index)
    {
        return attackAreas[index];
    }

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
                    building.GetComponent<Building>().TakeDamage(ta.damage);
                }
            }
            ta.hitBuildings.Clear();
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

    #endregion

}