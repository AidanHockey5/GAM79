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
    
    private float hInput, vInput, turnInput, attack1Input = 0;
    #endregion

    #region MonoBehaviours
    private void Start()
    {
        animator = GetComponent<Animator>();
        networkAnimator = GetComponent<NetworkAnimator>();
        Subscribe();
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
            animator.SetBool(animatorSettings.ISMOVING_BOOL, true);
            animator.SetFloat(animatorSettings.RUN_FLOAT, vInput);
            animator.SetFloat(animatorSettings.TURN_FLOAT, hInput);
        }
        else
        {
            animator.SetBool(animatorSettings.ISMOVING_BOOL, false);
            animator.SetFloat(animatorSettings.RUN_FLOAT, 0);
            animator.SetFloat(animatorSettings.TURN_FLOAT, 0);
        }
    }

    [Command]
    private void CmdJump()
    {

    }

    [Command]
    private void CmdFire1()
    {

    }

    [Command]
    private void CmdFire2()
    {

    }

    [Command]
    private void CmdFire3()
    {

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
                    CmdFire1();
                }
                break;
            case GameEvent.CHARACTER_FIRE2:
                {
                    CmdFire2();
                }
                break;
            case GameEvent.CHARACTER_FIRE3:
                {
                    CmdFire3();
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
