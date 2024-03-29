﻿using UnityEngine;
using System;
using System.Collections;

public class InputManager : MonoBehaviour, IEventBroadcaster
{

    #region Public Fields
    public event EventHandler<GameEventArgs> EventHandlerMainDelegate;
    public InputSettings input = new InputSettings();
	public bool isBroadcasting = true;
    #endregion

    #region Private Members
    private float hInput, vInput = 0;
	private float hMouseInput, vMouseInput, turnInput = 0;
    private float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput = 0;
	private bool fire1Input, fire2Input, fire3Input = false;
    #endregion

    #region Singleton
    private static InputManager instance_ = null;

    public static InputManager Instance
    {
        get
        {
            if (instance_ != null)
            {
                return instance_;
            }
            else
            {
                GameObject go = new GameObject();
                return instance_ = go.AddComponent<InputManager>();
            }
        }
    }
    #endregion

    #region MonoBehaviours
    void Awake()
    {
        instance_ = this;
    }

    void OnDestroy()
    {
        EventHandlerMainDelegate = null;
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
    #endregion

    #region GetInput Functions
    void GetInput()
    {
		if (!UIManager.Instance.IsPlayable())
			return;
		
        hMouseInput = Input.GetAxis(input.MOUSE_X_AXIS);
		vMouseInput = Input.GetAxis(input.MOUSE_Y_AXIS);
		turnInput = Input.GetAxis(input.ROTATE_AXIS);
		vOrbitInput = Input.GetAxis(input.ORBIT_VERTICAL);
		hOrbitInput = Input.GetAxis(input.ORBIT_VERTICAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
		fire1Input = Input.GetButton(input.ATTACK1);
		fire2Input = Input.GetButton(input.ATTACK2);
		fire3Input = Input.GetButton(input.ATTACK3);

		if (isBroadcasting) 
		{
			BroadcastEvent (GameEvent.MOUSE_X_INPUT, hMouseInput);
			BroadcastEvent (GameEvent.MOUSE_Y_INPUT, vMouseInput);

			if (turnInput != 0)
			{
				BroadcastEvent (GameEvent.CHARACTER_ROTATE, turnInput);
			}

			if (hOrbitInput != 0 || vOrbitInput != 0)
			{
				BroadcastEvent (GameEvent.CAMERA_ORBIT, hOrbitInput, -vOrbitInput);
			}

			if (hOrbitSnapInput > 0) 
			{ 
				BroadcastEvent (GameEvent.CAMERA_SNAP, hOrbitSnapInput);
			}

			if (zoomInput != 0)
			{
				BroadcastEvent (GameEvent.CAMERA_ZOOM, zoomInput);
			}

            //if (fire1Input)
            {
                BroadcastEvent(GameEvent.CHARACTER_FIRE1, fire1Input);
            }

			//if (fire2Input)
			{
				BroadcastEvent(GameEvent.CHARACTER_FIRE2, fire2Input);
			}

			//if (fire3Input)
			{
				BroadcastEvent(GameEvent.CHARACTER_FIRE3, fire3Input);
			}
		}
    }

    void GetInput_Fixed()
	{
		if (!UIManager.Instance.IsPlayable())
			return;
		
		if (isBroadcasting) 
		{
	        hInput = Input.GetAxis(input.HORIZONTAL_AXIS);
	        vInput = Input.GetAxis(input.VERTICAL_AXIS);

			if (hInput != 0 || vInput != 0)
	        {
	            BroadcastEvent(GameEvent.CHARACTER_MOVE, hInput, vInput);
	        }
		}
    }

    void GetInput_Late()
    {
		if (isBroadcasting)
		{
			
		}
    }
    #endregion

    #region IEventBroadcaster
    public void RegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate)
    {
        EventHandlerMainDelegate += EventHandlerDelegate;
    }

    public void UnRegisterHandler(EventHandler<GameEventArgs> EventHandlerDelegate)
    {
        EventHandlerMainDelegate -= EventHandlerDelegate;
    }

    public void BroadcastEvent(GameEvent eventType, params object[] args)
    {
        GameEventArgs gameEventArgs = new GameEventArgs();
        gameEventArgs.eventType = eventType;
        gameEventArgs.eventArgs = args;

        if (EventHandlerMainDelegate != null)
        {
            EventHandlerMainDelegate(this, gameEventArgs);
        }
    }
    #endregion

}
