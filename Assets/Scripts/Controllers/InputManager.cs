using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour, IEventBroadcaster
{
    public event EventHandler<EventArgs> m_handler;

    private static float horizontalInput, verticalInput, mouseHorizontalInput, mouseVerticalInput = 0;
    private static InputManager m_instance = null;

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

    void Awake()
    {
        m_instance = this;
    }

    void GetInput()
    {
        mouseHorizontalInput = Input.GetAxis("Mouse X");
        mouseVerticalInput   = Input.GetAxis("Mouse Y");
        if (mouseHorizontalInput != 0)
        {
            BroadcastMessage(GameEvent.CAMERA_ROTATE, mouseHorizontalInput);
        }
        if (mouseVerticalInput != 0)
        {
            BroadcastMessage(GameEvent.CHARACTER_ROTATE, mouseVerticalInput);
        }

    }

    void GetInput_Fixed()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        BroadcastMessage(GameEvent.CHARACTER_MOVE, horizontalInput, verticalInput);
    }

    void GetInput_Late()
    {

    }

    void BroadcastMessage(GameEvent eventType, params object[] args)
    {
        GameEventArgs gameEventArgs = new GameEventArgs();
        gameEventArgs.eventType = eventType;
        gameEventArgs.eventArgs = args;

        if (m_handler != null)
        {
            m_handler(this, gameEventArgs);
        }
    }

    void Update()
    {
        GetInput();
    }

    void LateUpdate()
    {
        GetInput_Late();
    }

    void FixedUpdate()
    {
        GetInput_Fixed();
    }

    public void RegisterHandler(EventHandler<EventArgs> p_handler)
    {
        m_handler += p_handler;
    }

    public void UnRegisterHandler(EventHandler<EventArgs> p_handler)
    {
        m_handler -= p_handler;
    }
}
