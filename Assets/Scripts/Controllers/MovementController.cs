using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour, IEventListener
{
    [SerializeField]
    private PlayerSettings playerSettings;
    [SerializeField]
    private AnimatorSettings animatorSettings;


    // required components
    private Rigidbody m_rigidbody = null;
    private Animator m_animator = null;

    // cached variables
    private Quaternion m_targetRotation;
	private float m_hInput, m_vInput, m_turnInput, m_attack1Input;
    private Vector3 m_targetVelocity, m_velocityChange;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
		Subscribe(this, Run);
		Subscribe(this, Turn);
        Subscribe(this, LightAttack);
		CameraController.Instance.SetCameraTarget(transform);
    }

    private void OnDestroy()
    {
		UnSubscribe(this, Run);
		UnSubscribe(this, Turn);
        UnSubscribe(this, LightAttack);
    }

    private void OnAnimatorMove()
    {
        switch (animatorSettings.currentState)
        {
            case MonsterState.IDLE:
                {
                    m_rigidbody.velocity = new Vector3(0, m_rigidbody.velocity.y, 0);
                }
                break;
            case MonsterState.MOVING:
                { 
                    // determine new direction
                    m_targetVelocity = new Vector3(0, 0, m_vInput);
                    m_targetVelocity = transform.TransformDirection(m_targetVelocity);
                    m_targetVelocity.Normalize();
                    // add speed
                    m_targetVelocity *= playerSettings.accelerationRate * m_animator.GetFloat(animatorSettings.SPEED_CURVE);
                    m_velocityChange = m_targetVelocity - m_rigidbody.velocity;
                    // limit to max speed
                    m_velocityChange.x = Mathf.Clamp(m_velocityChange.x, -playerSettings.maxVelocityChange, playerSettings.maxVelocityChange);
                    m_velocityChange.z = Mathf.Clamp(m_velocityChange.z, -playerSettings.maxVelocityChange, playerSettings.maxVelocityChange);
                    m_velocityChange.y = m_rigidbody.velocity.y;
                    // apply
                    Debug.Log(m_velocityChange);
                    m_rigidbody.AddForce(m_velocityChange, ForceMode.VelocityChange);
                }
                break;
            case MonsterState.ATTACKING:
                {
                    if (m_animator.GetBool(animatorSettings.ISATTACKING_BOOL))
                    {
                        animatorSettings.currentState = MonsterState.IDLE;
                        m_animator.SetBool(animatorSettings.ISATTACKING_BOOL, false);
                    }
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

            m_hInput = (float)gameEventArgs.eventArgs[0];
            m_vInput = (float)gameEventArgs.eventArgs[1];

            if (Mathf.Abs(m_hInput) > playerSettings.inputDelay || Mathf.Abs(m_vInput) > playerSettings.inputDelay)
            {
                animatorSettings.currentState = MonsterState.MOVING;
                m_animator.SetBool(animatorSettings.ISMOVING_BOOL, true);
                m_animator.SetFloat(animatorSettings.RUN_STRING, m_vInput);
                //m_animator.SetFloat(animatorSettings.TURN_STRING, m_hInput);
            }
            else
            {
                // zero x and z velocity while allowing default unity gravity
                animatorSettings.currentState = MonsterState.IDLE;
                m_animator.SetBool(animatorSettings.ISMOVING_BOOL, false);
                m_animator.SetFloat(animatorSettings.RUN_STRING, 0);
                //m_animator.SetFloat(animatorSettings.TURN_STRING, 0);
            }

            // tell server that i moved - NYI NYI NYI
        }
    }

	void Turn(object sender, GameEventArgs gameEventArgs)
	{
        // gameEventArgs[0] - turnInput

        if (gameEventArgs.eventType == GameEvent.CHARACTER_ROTATE)
		{
			m_turnInput = (float)gameEventArgs.eventArgs[0];

			if (Mathf.Abs(m_turnInput) > playerSettings.inputDelay && animatorSettings.currentState == MonsterState.IDLE)
			{
				m_targetRotation = Quaternion.AngleAxis(playerSettings.turnRate * m_turnInput * Time.deltaTime, Vector3.up);
				transform.rotation *= m_targetRotation;
			}
		}
	}

    void LightAttack(object sender, GameEventArgs gameEventArgs)
    {
        // gameEventArgs[0] - attack1Input

        if (gameEventArgs.eventType == GameEvent.CHARACTER_FIRE1)
        {
            m_attack1Input = (float)gameEventArgs.eventArgs[0];
            animatorSettings.currentState = MonsterState.ATTACKING;
            m_animator.SetBool(animatorSettings.ISATTACKING_BOOL, true);
        }
    }

	// IEventListener implementation
	public void Subscribe(object subscriber, EventHandler<GameEventArgs> handler)
	{
		InputManager.Instance.RegisterHandler(handler);
	}

	public void UnSubscribe(object subscriber, EventHandler<GameEventArgs> handler)
	{
		InputManager.Instance.UnRegisterHandler(handler);	
	}

}
