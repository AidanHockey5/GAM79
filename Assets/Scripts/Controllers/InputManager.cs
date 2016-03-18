using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour, IEventBroadcaster
{
    public event EventHandler<EventArgs> m_handler;
    public InputSettings input = new InputSettings();
	public bool isBroadcasting = true;
    
    // singleton
    public static InputManager Instance
    {
        get
        {
            if (m_instance != null)
            {
                return m_instance;
            }
            else
            {
                GameObject go = new GameObject();
                return m_instance = go.AddComponent<InputManager>();
            }
        }
    }

    private static InputManager m_instance = null;
    // axis input
    private static float m_hInput, m_vInput = 0;
    // mouse input
	private static float m_mouseHorizontalInput, m_mouseVerticalInput, m_turnInput = 0;
    // camera input
    private float m_vOrbitInput, m_hOrbitInput, m_zoomInput, m_hOrbitSnapInput = 0;

    void Awake()
    {
        m_instance = this;
    }

    void OnApplicationQuit()
    {
        m_handler = null;
    }

    void GetInput()
    {
		// character movement
        m_mouseHorizontalInput = Input.GetAxis(input.MOUSE_X_AXIS);
		m_mouseVerticalInput = Input.GetAxis(input.MOUSE_Y_AXIS);
		m_turnInput = Input.GetAxis(input.ROTATE_AXIS);

		// camera orbit
		m_vOrbitInput = Input.GetAxis(input.ORBIT_VERTICAL);
		m_hOrbitInput = Input.GetAxis(input.ORBIT_VERTICAL);
        m_hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        m_zoomInput = Input.GetAxisRaw(input.ZOOM);

		if (isBroadcasting) 
		{
			BroadcastEvent (GameEvent.MOUSE_X_INPUT, m_mouseHorizontalInput);
			BroadcastEvent (GameEvent.MOUSE_Y_INPUT, m_mouseVerticalInput);

			if (m_turnInput != 0)
			{
				BroadcastEvent (GameEvent.CHARACTER_ROTATE, m_turnInput);
			}

			if (m_hOrbitInput != 0 || m_vOrbitInput != 0)
			{
				BroadcastEvent (GameEvent.CAMERA_ORBIT, m_hOrbitInput, -m_vOrbitInput);
			}

			if (m_hOrbitSnapInput > 0) 
			{ 
				BroadcastEvent (GameEvent.CAMERA_SNAP, m_hOrbitSnapInput);
			}

			if (m_zoomInput != 0)
			{
				BroadcastEvent (GameEvent.CAMERA_ZOOM, m_zoomInput);
			}
		}
    }

    void GetInput_Fixed()
	{
		if (isBroadcasting) 
		{
	        m_hInput = Input.GetAxis(input.HORIZONTAL_AXIS);
	        m_vInput = Input.GetAxis(input.VERTICAL_AXIS);

			if (m_hInput != 0 || m_vInput != 0)
	        {
	            BroadcastEvent(GameEvent.CHARACTER_MOVE, m_hInput, m_vInput);
	        }
		}
    }

    void GetInput_Late()
    {
		if (isBroadcasting)
		{
			
		}
    }

    void Update()
    {
        GetInput();
    }

    void LateUpdate()
    {
        //GetInput_Late();
    }

    void FixedUpdate()
    {
        GetInput_Fixed();
    }

    // IEventBroadcaster implemenation
    public void RegisterHandler(EventHandler<EventArgs> p_handler)
    {
        m_handler += p_handler;
    }

    public void UnRegisterHandler(EventHandler<EventArgs> p_handler)
    {
        m_handler -= p_handler;
    }

    public void BroadcastEvent(GameEvent eventType, params object[] args)
    {
        GameEventArgs gameEventArgs = new GameEventArgs();
        gameEventArgs.eventType = eventType;
        gameEventArgs.eventArgs = args;

        if (m_handler != null)
        {
            m_handler(this, gameEventArgs);
        }
    }
}
