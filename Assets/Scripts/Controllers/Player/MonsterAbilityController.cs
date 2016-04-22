using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class MonsterAbilityController : NetworkBehaviour, IEventListener
{
	#region Public Fields
	public List<GameObject> hitBuildings, hitPlayers;
	public bool DOT;
	public int damage;
	public float DOTtimer;
	#endregion
	#region Private Members
	float timeCounter;
	TelegraphedAttack ta;
	[SerializeField] GameObject[] attackAreas;
	#endregion
	#region MonoBehaviours
	void Start ()
	{
		hitBuildings = new List<GameObject>();
		hitPlayers = new List<GameObject>();
		gameObject.SetActive(false);
		timeCounter = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.GetComponent<Building>() != null) 
		{
			hitBuildings.Add (col.gameObject);
		}

		if (col.gameObject.GetComponent<PlayerObject> () != null) 
		{
			hitPlayers.Add (col.gameObject);
		}
	}

	void OnTriggerExit(Collider col)
	{

	}

	void OnTriggerStay(Collider col)
	{
		if (DOT)
		{
			PlayerObject po = col.gameObject.GetComponent<PlayerObject> ();

			if (po)
			{
				timeCounter = timeCounter + Time.deltaTime;
				if (timeCounter > DOTtimer)
				{
					po.RequestTakeDamage(GameEvent.HIT_FROM_MONSTER, damage);
					timeCounter = 0;
				}
			}
		}
	}

	void OnEnable()
	{
		timeCounter = 0;
	}
	#endregion

	#region Netcode
	private void CmdSwipe(bool fire1Input)
	{
		
	}

	private void CmdGroundPound(bool fire2Input)
	{
		
	}

	private void CmdLavaBreath(bool fire3Input)
	{

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
			}
		}
	}

	public void ReceiveBroadcast(object sender, GameEventArgs gameEventArgs)
	{
		switch (gameEventArgs.eventType)
		{
		case GameEvent.CHARACTER_FIRE1:
			{
				CmdSwipe ((bool)gameEventArgs.eventArgs[0]);
			}
			break;
		case GameEvent.CHARACTER_FIRE2:
			{
				CmdGroundPound ((bool)gameEventArgs.eventArgs[0]);
			}
			break;
		case GameEvent.CHARACTER_FIRE3:
			{
				CmdLavaBreath ((bool)gameEventArgs.eventArgs[0]);
			}
			break;
		}
	}
	#endregion



	public void ActivateAttackArea(int attackAreaIndex)
	{
		attackAreas[attackAreaIndex].SetActive(true);
	}

	public void DeactivateAttackArea(int attackAreaIndex)
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