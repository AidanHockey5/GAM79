using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour, IEventBroadcaster
{
    public event EventHandler<EventArgs> m_handler;
    public InputSettings input = new InputSettings();
    
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
    private static float m_horizontalInput, m_verticalInput = 0;

    // mouse input
    private static float m_mouseHorizontalInput, m_mouseVerticalInput = 0;

    // camera input
    private float m_vOrbitInput, m_hOrbitInput, m_zoomInput, m_hOrbitSnapInput = 0;

    void Awake()
    {
        m_instance = this;
    }

    void OnDestroy()
    {
        m_handler = null;
    }

    void GetInput()
    {
        m_mouseHorizontalInput = Input.GetAxis(input.MOUSE_X_AXIS);
        m_mouseVerticalInput = Input.GetAxis(input.MOUSE_Y_AXIS);
        m_vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        m_hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        m_hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        m_zoomInput = Input.GetAxisRaw(input.ZOOM);
        BroadcastEvent(GameEvent.MOUSE_X_INPUT, m_mouseHorizontalInput);
        BroadcastEvent(GameEvent.MOUSE_Y_INPUT, m_mouseVerticalInput);
        BroadcastEvent(GameEvent.CAMERA_ORBIT, m_hOrbitInput, m_vOrbitInput);
        if (m_hOrbitSnapInput > 1)
        { 
            BroadcastEvent(GameEvent.CAMERA_SNAP, m_hOrbitSnapInput);
        }
        BroadcastEvent(GameEvent.CAMERA_ZOOM, m_zoomInput);
    }

    void GetInput_Fixed()
    {
        m_horizontalInput = Input.GetAxis(input.HORIZONTAL_AXIS);
        m_verticalInput = Input.GetAxis(input.VERTICAL_AXIS);

        if (m_horizontalInput != 0 || m_verticalInput != 0)
        {
            BroadcastEvent(GameEvent.CHARACTER_MOVE, m_horizontalInput, m_verticalInput);
        }
    }

    void GetInput_Late()
    {

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
