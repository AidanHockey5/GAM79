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
	   // InstanceManager.Register(this);
	    InstanceManager.Register(this);
    //    AudioManager.audManInst.PlayMusic(musicTrack);
	}
		
	void Start()
	{
	    maxTicketAmount = 10;
	    currentTicketAmount = maxTicketAmount;
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
        if (currentTicketAmount <= 0)
        {
            print("Game is Over");
        }
	}
}
