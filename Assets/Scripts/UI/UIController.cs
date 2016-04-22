using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour 
{
	protected PlayerData _data;

	public void UpdatePlayer(PlayerData data)
	{
		_data = data;
		Health = _data.health;
		Shield = _data.shield;
		Clip = _data.clip;
	}

	public int Health { get; set; }
	public int Shield { get; set; }
	public int Clip { get; set; }
}
