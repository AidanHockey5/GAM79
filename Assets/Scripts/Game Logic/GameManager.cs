using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[Singleton]
public class GameManager : MonoBehaviour
{
	public Text healthText;
   	public RectTransform healthBar;
   	public int maxTicketAmount = 0;
   	public int currentTicketAmount = 0;

	public  void Awake()
	{
	    InstanceManager.Register(this);
	}

	void Start()
	{
	    maxTicketAmount = 10;
	    currentTicketAmount = maxTicketAmount;
	}
	public void SetHealthText(int currentHealth, int maxHealth)
	{
		if(healthText != null)
			healthText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth;
	}
	void OnChangeHealth(int health)
	{
		if(healthBar != null)
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
	public void GameOver()
	{
	    print("Game is Over");
	}
}
