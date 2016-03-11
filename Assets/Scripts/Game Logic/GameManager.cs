using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour 
{
	public Text healthText;
    public int maxTicketAmount = 0;
    public int currentTicketAmount = 0;

    void Start()
    {
        maxTicketAmount = 10;
        currentTicketAmount = maxTicketAmount;
    }
	public void SetHealthText(int amount)
	{
		healthText.text = "Health: " + amount.ToString();
	}

    public void OnPlayerDeath(int ticket)
    {
         currentTicketAmount -= ticket;
    }

}
