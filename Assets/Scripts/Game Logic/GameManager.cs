using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AudioClip musicTrack = null;
    
    public Text healthText;
   	public RectTransform healthBar;
   	public int maxTicketAmount = 0;
   	public int currentTicketAmount = 0;
    public Text humanHealthText, monsterHealthText;             // Placeholders for Health Texts for Health.cs to utilize
    public RectTransform humanHealthBar, monsterHealthBar;      // Placeholders for Health Bars for Health.cs to utilize

	private static GameManager instance_ = null;

	public static GameManager Instance
	{
		get
		{
			if (instance_ != null)
			{
				return instance_;
			}
			else
			{
				GameObject go = new GameObject();
				return instance_ = go.AddComponent<GameManager>();
			}
		}
	}

	public  void Awake()
	{
	   // InstanceManager.Register(this);
		instance_ = this;
    //    AudioManager.audManInst.PlayMusic(musicTrack);
	}
		
	void Start()
	{
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
