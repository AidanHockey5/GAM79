using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public RectTransform mainMenuPanel;
	public RectTransform gamesListPanel;
	public InputField directConnectInput;
	public RectTransform playerSelectionPanel;

	private RectTransform _currentPanel;

	void Start () 
	{
		_currentPanel = mainMenuPanel;
	}

	public void ChangeTo(RectTransform newPanel)
	{
		if (_currentPanel != null) 
		{
			_currentPanel.gameObject.SetActive (false);
		}
		if (newPanel != null) 
		{
			newPanel.gameObject.SetActive (true);
		}

		_currentPanel = newPanel;

		if (_currentPanel == mainMenuPanel)
		{
			CustomNetworkManager.Instance.StopBroadcasting ();
		}
	}

	public void OnClickBackButton()
	{
		ChangeTo (mainMenuPanel);
	}

	public void OnClickHostGame()
	{
		CustomNetworkManager.Instance.StartHosting ();
		ChangeTo (playerSelectionPanel);
	}

	public void OnClickFindGame()
	{
		ChangeTo (gamesListPanel);
		CustomNetworkManager.Instance.StartListening ();
	}

	public void OnClickConnect()
	{
		CustomNetworkManager.Instance.JoinMatch (directConnectInput.text);
	}

	public void OnClickMonster()
	{
		CustomNetworkManager.Instance.SetMonser ();
	}

	public void OnClickHuman()
	{
		CustomNetworkManager.Instance.SetHuman ();
	}

}
