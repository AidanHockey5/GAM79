using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NetworkAnimator))]
public class AnimatorCharacterController : NetworkBehaviour
{

    #region Public Fields

    #endregion

    #region Private Members
    private AnimatorSettings animatorSettings = null;
    private MovementSettings movementSettings = null;
    private Animator animator = null;
    private NetworkAnimator networkAnimator = null;
    #endregion

    #region MonoBehaviours
    private void Start()
    {
        animator = GetComponent<Animator>();
        networkAnimator = GetComponent<NetworkAnimator>();
		if (isLocalPlayer)
		{
			Subscribe();
		}
		else
		{
			animator.applyRootMotion = false;
		}
    }

    private void OnDestroy()
    {
        UnSubscribe();
    }
    #endregion

    #region Net Code
    [Command]
    private void CmdMove(float hInput, float vInput)
    {
        if (Mathf.Abs(hInput) > movementSettings.inputDelay || Mathf.Abs(vInput) > movementSettings.inputDelay)
        {
            animator.SetFloat(animatorSettings.RUN_FLOAT, vInput);
            animator.SetFloat(animatorSettings.TURN_FLOAT, hInput);
        }
        else
        {
            animator.SetFloat(animatorSettings.RUN_FLOAT, 0);
            animator.SetFloat(animatorSettings.TURN_FLOAT, 0);
        }
    }

    [Command]
    private void CmdJump()
    {

    }

    [Command]
	private void CmdFire1(bool fire1Input)
    {
		if (animatorSettings.currentState == MonsterState.IDLE) 
		{
			networkAnimator.SetTrigger (animatorSettings.FIRE_1_TRIGGER);
			animator.SetBool (animatorSettings.FIRE_1_BOOL, fire1Input);
		} 
		else if (animatorSettings.currentState == MonsterState.ATTACKING) 
		{
			animator.SetBool (animatorSettings.FIRE_1_BOOL, fire1Input);
		}
    }

    [Command]
	private void CmdFire2(bool fire2Input)
    {
		if (animatorSettings.currentState == MonsterState.IDLE) 
		{
			networkAnimator.SetTrigger (animatorSettings.FIRE_2_TRIGGER);
			animator.SetBool (animatorSettings.FIRE_2_BOOL, fire2Input);
		} 
		else if (animatorSettings.currentState == MonsterState.ATTACKING) 
		{
			animator.SetBool (animatorSettings.FIRE_2_BOOL, fire2Input);
		}
    }

    [Command]
	private void CmdFire3(bool fire3Input)
    {
		if (animatorSettings.currentState == MonsterState.IDLE) 
		{
			networkAnimator.SetTrigger (animatorSettings.FIRE_3_TRIGGER);
			animator.SetBool (animatorSettings.FIRE_3_BOOL, fire3Input);
		} 
		else if (animatorSettings.currentState == MonsterState.ATTACKING) 
		{
			animator.SetBool (animatorSettings.FIRE_3_BOOL, fire3Input);
		}
    }

    [Command]
    private void CmdFireSpecial()
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
                animatorSettings = playerObj.animatorSettings;
				movementSettings = playerObj.movementSettings;
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
                animatorSettings = null;
                movementSettings = null;
            }
        }
    }

    public void ReceiveBroadcast(object sender, GameEventArgs gameEventArgs)
    {
        switch (gameEventArgs.eventType)
        {
            case GameEvent.CHARACTER_MOVE:
                {
                    // index 0 = hInput, index 1 = vInput
                    CmdMove((float)gameEventArgs.eventArgs[0], (float)gameEventArgs.eventArgs[1]);
                }
                break;
            case GameEvent.CHARACTER_JUMP:
                {
                    CmdJump();
                }
                break;
            case GameEvent.CHARACTER_FIRE1:
                {
					// index 0 - fire1Input
					CmdFire1((bool)gameEventArgs.eventArgs[0]);
                }
                break;
            case GameEvent.CHARACTER_FIRE2:
                {
					// index 0 - fire2Input
					CmdFire2((bool)gameEventArgs.eventArgs[0]);
                }
                break;
            case GameEvent.CHARACTER_FIRE3:
                {
					// index 0 - fire3Input
					CmdFire3((bool)gameEventArgs.eventArgs[0]);
                }
                break;
            case GameEvent.CHARACTER_FIRESPECIAL:
                {
                    CmdFireSpecial();
                }
                break;
        }
    }

    #endregion

}
