using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[Singleton]
public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
=======
    public AudioClip musicTrack = null;
    
    public Text healthText;
   	public RectTransform healthBar;
>>>>>>> AudioPartDeux
   	public int maxTicketAmount = 0;
   	public int currentTicketAmount = 0;
    public Text humanHealthText, monsterHealthText;             // Placeholders for Health Texts for Health.cs to utilize
    public RectTransform humanHealthBar, monsterHealthBar;      // Placeholders for Health Bars for Health.cs to utilize

	public  void Awake()
	{
<<<<<<< HEAD
	    //InstanceManager.Register(this);
=======
	    InstanceManager.Register(this);
        AudioManager.audManInst.PlayMusic(musicTrack);
>>>>>>> AudioPartDeux
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
	    print("Game is Over");
	}
}
