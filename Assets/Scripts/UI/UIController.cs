using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour 
{
	protected PlayerData _data;

	public void UpdatePlayer(PlayerData data)
	{
		_data = data;
	}
}
