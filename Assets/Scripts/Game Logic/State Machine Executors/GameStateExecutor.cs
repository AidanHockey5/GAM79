﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateExecutor : MonoBehaviour 
{
	public List<GameState> gameStates = new List<GameState>();
	StateMachineEngine stateMachine = null;

	#region MONOBEHAVIORS
	void Awake()
	{
		//Looks for state machine engine.
		stateMachine = GameObject.FindObjectOfType<StateMachineEngine> () as StateMachineEngine;
	}

	void Start () 
	{
		//Associates object to selected state enum then changes state to default.
		stateMachine.Initialize<GameState> (this);
		stateMachine.ChangeState (GameState.RUNNING);
	}

	void Update () 
	{
		//Gets lists of all states in enum.
		gameStates = stateMachine.GetStates<GameState> ();
	}
	#endregion

	#region RUNNING
	public void RUNNING_Enter()
	{
		//Runs when object enters state.
	}

	public void RUNNING_Update()
	{
		//Works just like regular update, but only runs while the state is active.
		Debug.Log("Game is running!");
	}

	public void RUNNING_Exit()
	{
		//Runs when object exits state.
	}
	#endregion

	#region PAUSED
	public void PAUSED_Enter()
	{
		
	}

	public void PAUSED_Update()
	{
		Debug.Log("Game is paused!");
	}

	public void PAUSED_Exit()
	{
		
	}
	#endregion

	#region WIN
	public void WIN_Enter()
	{
		
	}

	public void WIN_Update()
	{
		Debug.Log("Win condition met!");
	}

	public void WINExit()
	{
		
	}
	#endregion

	#region LOSS
	public void LOSS_Enter()
	{
		//Runs when object enters state.
	}

	public void LOSS_Update()
	{
		Debug.Log("Lose condition met!");
	}

	public void LOSS_Exit()
	{
		
	}
	#endregion

}
