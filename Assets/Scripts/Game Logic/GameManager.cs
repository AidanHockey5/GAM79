using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[Singleton]
public class GameManager : MonoBehaviour
{
    public AudioClip musicTrack = null;
    
    public Text healthText;
   	public RectTransform healthBar;
   	public int maxTicketAmount = 0;
   	public int currentTicketAmount = 0;
    public Text humanHealthText, monsterHealthText;             // Placeholders for Health Texts for Health.cs to utilize
    public RectTransform humanHealthBar, monsterHealthBar;      // Placeholders for Health Bars for Health.cs to utilize

	public  void Awake()
	{
	    InstanceManager.Register(this);
        AudioManager.audManInst.PlayMusic(musicTrack);
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
