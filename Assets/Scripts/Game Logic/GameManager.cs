using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour, IEventBroadcaster
{
	[SerializeField]
	private int _ticketAmount;

	private int _currentTicketAmount = 0;
	private int _numberOfPlayers = 0;
	private int _playersEliminated;

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

	public bool CanSpawn(int ticketAmountNeeded)
	{
		return _currentTicketAmount - ticketAmountNeeded >= 0;
	}

	public void OnCharacterSpawn(int ticketAmount)
	{
		UpdateTicketAmount (-(ticketAmount));
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
		if (TicketsDepleted())
		{
			_playersEliminated++;
			if (AllPlayersEliminated ())
			{
				GameOver (GameOverState.TicketsDepleted);
			}
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
