using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour, IEventListener
{
    public PlayerSettings player = new PlayerSettings();

	private Quaternion m_targetRotation;
    private Rigidbody m_rigidbody = null;
	private float m_horizontalInput, m_verticalInput, m_turnInput;
    private Vector3 m_targetVelocity, m_velocityChange;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
		Subscribe(this, Run);
		Subscribe(this, Turn);
		CameraController.Instance.SetCameraTarget(transform);
    }

    private void OnDestroy()
    {
		UnSubscribe(this, Run);
		UnSubscribe(this, Turn);
    }

	public Quaternion TargetRotation
	{
		get 
		{ 
			return m_targetRotation;
		}
	}

	// IEventBroadcaster Callback
    void Run(object sender, EventArgs e)
    {
		// args[0] - hInput
		// args[1] - vInput
        GameEventArgs args = (GameEventArgs)e;

        if (args.eventType == GameEvent.CHARACTER_MOVE)
		{
            m_horizontalInput = (float)args.eventArgs[0];
            m_verticalInput = (float)args.eventArgs[1];

            if (Mathf.Abs(m_horizontalInput) > player.inputDelay || Mathf.Abs(m_verticalInput) > player.inputDelay)
            {
                // determine new direction
                m_targetVelocity = new Vector3(m_horizontalInput, 0, m_verticalInput);
                m_targetVelocity = transform.TransformDirection(m_targetVelocity);
                m_targetVelocity.Normalize();
                // add speed
                m_targetVelocity *= player.accelerationRate;
                m_velocityChange = m_targetVelocity - m_rigidbody.velocity;
                // limit to max speed
                m_velocityChange.x = Mathf.Clamp(m_velocityChange.x, -player.maxVelocityChange, player.maxVelocityChange);
                m_velocityChange.z = Mathf.Clamp(m_velocityChange.z, -player.maxVelocityChange, player.maxVelocityChange);
                m_velocityChange.y = 0;
                // apply
                m_rigidbody.AddForce(m_velocityChange, ForceMode.VelocityChange);
            }
            else
            {
                // zero x and z velocity while allowing default unity gravity
                m_rigidbody.velocity = new Vector3(0, m_rigidbody.velocity.y, 0);
            }

            // tell server that i moved - NYI NYI NYI
        }
    }

	void Turn(object sender, EventArgs e)
	{
		// args[0] - turnInput
		GameEventArgs args = (GameEventArgs)e;

		if (args.eventType == GameEvent.CHARACTER_ROTATE)
		{
			Debug.Log("ROTATE");
			m_turnInput = (float)args.eventArgs[0];

			if (Mathf.Abs(m_turnInput) > player.inputDelay)
			{
				m_targetRotation = Quaternion.AngleAxis(player.turnRate * m_turnInput * Time.deltaTime, Vector3.up);
				transform.rotation *= m_targetRotation;
			}
		}
	}

	// IEventListener implementation
	public void Subscribe(object subscriber, EventHandler<EventArgs> handler)
	{
		InputManager.Instance.RegisterHandler(handler);
	}

	public void UnSubscribe(object subscriber, EventHandler<EventArgs> handler)
	{
		InputManager.Instance.UnRegisterHandler(handler);	
	}

}
