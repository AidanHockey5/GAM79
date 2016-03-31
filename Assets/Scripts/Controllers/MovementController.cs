using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;

[RequireComponent(typeof(Animator))]
public class MovementController : NetworkBehaviour, IEventListener
{
    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private AnimatorSettings animSettings;
	[SerializeField]
	private CameraPositionSettings cameraPosSettings;
    [SerializeField]
    private MovementSettings moveSettings;


    // required components
    CharacterController charController = null;
    new private Transform transform = null;
    private Animator animator = null;

    // cached variables
    private Quaternion tarRotation;
	private float hInput, vInput, turnInput, attack1Input;
    private Vector3 tarDirection, velocityChange;

    private void Start()
    {
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
		Subscribe(this, Run);
		Subscribe(this, Turn);
        Subscribe(this, LightAttack);
		CameraController.Instance.SetCameraTarget(transform, cameraPosSettings);
    }

    private void OnDestroy()
    {
		UnSubscribe(this, Run);
		UnSubscribe(this, Turn);
        UnSubscribe(this, LightAttack);
    }

	[ClientCallback]
    private void OnAnimatorMove()
    {
        switch (animSettings.currentState)
        {
            case MonsterState.IDLE:
                {

                }
                break;
            case MonsterState.MOVING:
                { 
                    // determine new direction
                    tarDirection = new Vector3(0, 0, vInput);
                    tarDirection = transform.TransformDirection(tarDirection);
                    tarDirection.Normalize();
                    tarDirection *= moveSettings.accelerationRate * animator.GetFloat(animSettings.SPEED_CURVE);
                    tarDirection.z = Mathf.Clamp(tarDirection.z, -moveSettings.maxSpeed, moveSettings.maxSpeed);
                    tarDirection.y += moveSettings.gravity * Time.deltaTime;
                    charController.Move(tarDirection * Time.deltaTime);
                }
                break;
            case MonsterState.ATTACKING:
                {
                    if (animator.GetBool(animSettings.ISATTACKING_BOOL))
                    {
                        animSettings.currentState = MonsterState.IDLE;
                        animator.SetBool(animSettings.ISATTACKING_BOOL, false);
                    }
                }
                break;
            case MonsterState.DEAD:
                {

                }
                break;
        }
    }

	// IEventBroadcaster
    void Run(object sender, GameEventArgs gameEventArgs)
    {
        // gameEventArgs[0] - hInput
        // gameEventArgs[1] - vInput

        if (gameEventArgs.eventType == GameEvent.CHARACTER_MOVE)
		{

            hInput = (float)gameEventArgs.eventArgs[0];
            vInput = (float)gameEventArgs.eventArgs[1];

            if (Mathf.Abs(hInput) > moveSettings.inputDelay || Mathf.Abs(vInput) > moveSettings.inputDelay)
            {
                animSettings.currentState = MonsterState.MOVING;
                animator.SetBool(animSettings.ISMOVING_BOOL, true);
                animator.SetFloat(animSettings.RUN_FLOAT, vInput);
                animator.SetFloat(animSettings.TURN_FLOAT, hInput);
            }
            else
            {
                animSettings.currentState = MonsterState.IDLE;
                animator.SetBool(animSettings.ISMOVING_BOOL, false);
                animator.SetFloat(animSettings.RUN_FLOAT, 0);
                animator.SetFloat(animSettings.TURN_FLOAT, 0);
            }
        }
    }

	void Turn(object sender, GameEventArgs gameEventArgs)
	{
        // gameEventArgs[0] - turnInput

        if (gameEventArgs.eventType == GameEvent.CHARACTER_ROTATE)
		{
			turnInput = (float)gameEventArgs.eventArgs[0];

			if (Mathf.Abs(turnInput) > moveSettings.inputDelay && animSettings.currentState == MonsterState.IDLE)
			{
				tarRotation = Quaternion.AngleAxis(moveSettings.turnRate * turnInput * Time.deltaTime, Vector3.up);
				transform.rotation *= tarRotation;
			}
		}
	}

    void LightAttack(object sender, GameEventArgs gameEventArgs)
    {
        // gameEventArgs[0] - attack1Input

        if (gameEventArgs.eventType == GameEvent.CHARACTER_FIRE1)
        {
            attack1Input = (float)gameEventArgs.eventArgs[0];
            animSettings.currentState = MonsterState.ATTACKING;
            animator.SetBool(animSettings.ISATTACKING_BOOL, true);
        }
    }

    #region IEVENTLISTENER IMPLEMENATION
    public void Subscribe(object subscriber, EventHandler<GameEventArgs> handler)
	{
		if (isLocalPlayer) 
		{
			InputManager.Instance.RegisterHandler(handler);
		}
	}

	public void UnSubscribe(object subscriber, EventHandler<GameEventArgs> handler)
	{
		if (isLocalPlayer) 
		{
			InputManager.Instance.UnRegisterHandler(handler);
		}
	}
    #endregion
}
