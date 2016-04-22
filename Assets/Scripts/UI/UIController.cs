using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour 
{
	private PlayerData _data;

	public void UpdatePlayer(PlayerData data)
	{
		_data = data;
	}
}
