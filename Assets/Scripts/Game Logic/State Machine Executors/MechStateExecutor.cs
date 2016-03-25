using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MechStateExecutor : MonoBehaviour 
{
	public List<MechState> mechStates = new List<MechState> ();
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
		stateMachine.Initialize<MechState> (this);
		stateMachine.ChangeState (MechState.NONE);
	}

	void Update () 
	{
		//Gets lists of all states in enum.
		mechStates = stateMachine.GetStates<MechState> ();
	}
	#endregion

	#region NONE
	public void NONE_Enter()
	{
		//Runs when object enters state.
		Debug.Log(this.name + " : No state is selected!");
	}

	public void NONE_Update()
	{
		//Works just like regular update, but only runs while the state is active.
	}

	public void NONE_Exit()
	{
		//Runs when object exits state.
	}
	#endregion

	#region IDLE
	public void IDLE_Enter()
	{

	}

	public void IDLE_Update()
	{

	}

	public void IDLE_Exit()
	{

	}
	#endregion

	#region MOVING
	public void MOVING_Enter()
	{

	}

	public void MOVING_Update()
	{

	}

	public void MOVING_Exit()
	{

	}
	#endregion

	#region ATTACKING
	public void ATTACKING_Enter()
	{

	}

	public void ATTACKING_Update()
	{

	}

	public void ATTACKING_Exit()
	{

	}
	#endregion

	#region DEAD
	public void DEAD_Enter()
	{

	}

	public void DEAD_Update()
	{

	}

	public void DEAD_Exit()
	{

	}
	#endregion
}
