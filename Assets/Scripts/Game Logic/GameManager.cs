using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour 
{
	public Text healthText;
    public int maxTicketAmount = 0;
    public int currentTicketAmount = 0;

    // singleton
    public static GameManager Instance
    {
        get
        {
            if (m_instance != null)
            {
                return m_instance;
            }
            else
            {
                GameObject go = new GameObject();
                return m_instance = go.AddComponent<GameManager>();
            }
        }
    }

    private static GameManager m_instance = null;

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

         if (currentTicketAmount == 0)
         {
             print("You Failed");
         }
    }

}
