using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour 
{
	public Text healthText;
    public RectTransform healthBar;
    public int maxTicketAmount = 0;
    public int currentTicketAmount = 0;

    void Start()
    {
        maxTicketAmount = 10;
        currentTicketAmount = maxTicketAmount;
    }
	public void SetHealthText(int currentHealth, int maxHealth)
	{
		healthText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth;
	}
    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
    }

    public void OnPlayerDeath(int ticket)
    {
         currentTicketAmount -= ticket;

         if (currentTicketAmount == 0)
         {
             print("You Failed");
         }
    }

}
