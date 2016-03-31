using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GameManager : NetworkBehaviour 
{
    public Text humanHealthText, monsterHealthText;             // Placeholders for Health Texts for Health.cs to utilize
    public RectTransform humanHealthBar, monsterHealthBar;      // Placeholders for Health Bars for Health.cs to utilize
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
