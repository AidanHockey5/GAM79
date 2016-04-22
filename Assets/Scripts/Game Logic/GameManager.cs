using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour, IEventBroadcaster
{
	[SerializeField]
	private GameObject _technicianPrefab;
	[SerializeField]
	private GameObject _supportPrefab;
	[SerializeField]
	private GameObject _heavyPrefab;
	[SerializeField]
	private GameObject _assaultPrefab;

	[SerializeField]
	private int _ticketAmount;

	[SyncVar]
	private int _currentTicketAmount = 0;

	private int _numberOfPlayers = 0;
	private int _playersEliminated;

	private PlayerControl _playerControl;

	private static GameManager instance_ = null;

	public static GameManager Instance
	{
		get
		{
			if (instance_ != null)
			{
				return instance_;
			}
			else
			{
				GameObject go = new GameObject();
				return instance_ = go.AddComponent<GameManager>();
			}
		}
	}

	public  void Awake()
	{
		instance_ = this;
	}
		
	void Start()
	{
	    _currentTicketAmount = _ticketAmount;
	}

	void Update()
	{
		//Call player select screen
		if(Input.GetKeyDown(KeyCode.P))
		{
			UIManager.Instance.SetUIState(UIState.CharacterSelect);
		}
	}

	public void RegisterPlayer(PlayerControl pControl)
	{
		_playerControl = pControl;
	}

	public void SpawnSupport()
	{
		if (CanSpawn (1))
		{
			_playerControl.SpawnClass (SpawnClass.Support);
			UIManager.Instance.SetUIState (UIState.Support);
		}
	}

	public void SpawnTechnician()
	{
		if (CanSpawn (1))
		{
			_playerControl.SpawnClass (SpawnClass.Technician);
			UIManager.Instance.SetUIState (UIState.Technician);
		}
	}

	public void SpawnHeavy()
	{
		if (CanSpawn (3))
		{
			_playerControl.SpawnClass (SpawnClass.Heavy);
			UIManager.Instance.SetUIState (UIState.Heavy);
		}
	}

	public void SpawnAssault()
	{
		if (CanSpawn (3))
		{
			_playerControl.SpawnClass (SpawnClass.Assault);
			UIManager.Instance.SetUIState (UIState.Assault);
		}
	}

	public bool CanSpawn(int ticketAmountNeeded)
	{
		return _currentTicketAmount - ticketAmountNeeded >= 0;
	}

	public void OnCharacterSpawn(SpawnClass sClass)
	{
		int cost = 0;
		switch (sClass)
		{
		case SpawnClass.Technician: 
		case SpawnClass.Support:
			cost = 1;
			break;
		case SpawnClass.Heavy:
		case SpawnClass.Assault:
			cost = 3;
			break;
		}
		UpdateTicketAmount (-(cost));
	}

	private void UpdateTicketAmount(int amount)
	{
		_currentTicketAmount += amount;
	}

	public void MainObjectiveComplete()
	{
		GameOver (GameOverState.ObjectiveDestroyed);
	}

	public bool TicketsDepleted()
	{
		return _currentTicketAmount <= 0;
	}

	public bool AllPlayersEliminated()
	{
		return _playersEliminated == _numberOfPlayers;
	}


	public void OnPlayerDeath()
	{
		if (TicketsDepleted ())
		{
			_playersEliminated++;
			if (AllPlayersEliminated ())
			{
				GameOver (GameOverState.TicketsDepleted);
			}
		}
		else
		{
			UIManager.Instance.SetUIState (UIState.CharacterSelect);
		}
	}

	public void OnMonsterDeath()
	{
		GameOver (GameOverState.MonsterDeath);
	}

	public void GameOver(GameOverState state)
	{
		//TODO: Trigger GameOver UIs
		switch (state)
		{
		case GameOverState.ObjectiveDestroyed:
			break;
		case GameOverState.MonsterDeath:
			break;
		case GameOverState.TicketsDepleted:
			break;
		}
	}

	public void AddPlayer()
	{
		_numberOfPlayers++;
	}

	public void RemovePlayer()
	{
		_numberOfPlayers--;
		if (_playersEliminated > 0)
			_playersEliminated--;
	}

	#region IEventBroadcaster
	public event EventHandler<GameEventArgs> EventHandlerMainDelegate;

	public void RegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate)
	{
		EventHandlerMainDelegate += EventHandlerDelegate;
	}
	public void UnRegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate)
	{
		EventHandlerMainDelegate -= EventHandlerDelegate;
	}

	public void BroadcastEvent(GameEvent eventType, params object[] args)
	{
		GameEventArgs gameEventArgs = new GameEventArgs();
		gameEventArgs.eventType = eventType;
		gameEventArgs.eventArgs = args;

		if (EventHandlerMainDelegate != null)
		{
			EventHandlerMainDelegate(this, gameEventArgs);
		}
	}
	#endregion
}
