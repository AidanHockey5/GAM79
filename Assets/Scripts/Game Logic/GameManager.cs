using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour 
{
	public Text healthText;

	public void SetHealthText(int amount)
	{
		healthText.text = "Health: " + amount.ToString();
	}

}
