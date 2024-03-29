﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour 
{
	private RectTransform _current;
	[SerializeField]
	private RectTransform _characterSelect;
	[SerializeField]
	private RectTransform _monster;
	[SerializeField]
	private RectTransform _technician;
	[SerializeField]
	private RectTransform _support;
	[SerializeField]
	private RectTransform _heavy;
	[SerializeField]
	private RectTransform _assault;
	[SerializeField]
	private RectTransform _monsterLose;
	[SerializeField]
	private RectTransform _monsterWin;
	[SerializeField]
	private RectTransform _humanLose;
	[SerializeField]
	private RectTransform _humanWin;

	private Dictionary<UIState, UIController> _panels = new Dictionary<UIState, UIController>();
	private UIState _currentState = UIState.Technician;
	private UIState _previousState = UIState.Technician;

	private static UIManager instance_ = null;

	public static UIManager Instance
	{
		get
		{
			if (instance_ != null)
			{
				return instance_;
			}
			else
			{
				GameObject go = new GameObject ();
				return instance_ = go.AddComponent<UIManager> ();
			}
		}
	}

	void Awake()
	{
		instance_ = this;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && _current == _characterSelect)
		{
			SetUIState (_previousState);
		}
	}

	public void RegisterController(UIState state, UIController controller)
	{
		_panels.Add (state, controller);
	}

	public void SetUIState(UIState state)
	{
		_previousState = _currentState;
		_currentState = state;
		switch (state)
		{
		case UIState.CharacterSelect:
			ChangeTo (_characterSelect);
			break;
		case UIState.Monster:
			ChangeTo (_monster);
			break;
		case UIState.Technician:
			ChangeTo (_technician);
			break;
		case UIState.Support:
			ChangeTo (_support);
			break;
		case UIState.Heavy:
			ChangeTo (_heavy);
			break;
		case UIState.Assault:
			ChangeTo (_assault);
			break;
		case UIState.MonsterWin:
			ChangeTo (_monsterWin);
			break;
		case UIState.MonsterLose:
			ChangeTo (_monsterLose);
			break;
		case UIState.HumansWin:
			ChangeTo (_humanWin);
			break;
		case UIState.HumansLose:
			ChangeTo (_humanLose);
			break;
		}
	}

	private void ChangeTo(RectTransform newPanel)
	{
		if (_current != null) 
		{
			_current.gameObject.SetActive (false);
		}
		if (newPanel != null) 
		{
			newPanel.gameObject.SetActive (true);
		}

		_current = newPanel;

		if (_current == _characterSelect)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		
	}

	public bool IsPlayable()
	{
		return _currentState == UIState.Monster || _currentState == UIState.Technician || _currentState == UIState.Support
		|| _currentState == UIState.Heavy || _currentState == UIState.Assault;

	}
	public void UpdatePlayerData(PlayerData data)
	{
		_panels [_currentState].UpdatePlayer (data);
	}
}

public enum UIState
{
	CharacterSelect,
	Monster,
	Technician,
	Support,
	Heavy,
	Assault,
	MonsterWin,
	MonsterLose,
	HumansWin,
	HumansLose
}
