using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class Health : NetworkBehaviour
{
    public GameManager gManager;
    public SpawnPointManager spawnManager;
    public int max;
    public int currentHealth;
    public int ticket = 1;

    void Start()
    {
		gManager = InstanceManager.GetInstance<GameManager> ();
		spawnManager = InstanceManager.GetInstance<SpawnPointManager> ();

        if (gManager == null)
        {
            //gameObject.SetActive(false);
            Debug.LogError("There is no active GameManager in scene.");
        }
        else
        {
            currentHealth = max;
            gManager.SetHealthText(currentHealth, max);
        }
    }

    void Update()
    {
		if (gManager == null)
		{
			gManager = InstanceManager.GetInstance<GameManager> ();
			gManager.SetHealthText(currentHealth, max);
		}

		if (spawnManager == null)
		{
			spawnManager = InstanceManager.GetInstance<SpawnPointManager> ();
		}

        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(10);
        }
    }
    public void TakeDamage(int amount)
    {
//		if (!isLocalPlayer)
//			return;
		
        currentHealth -= amount;

		if(gManager != null)
			gManager.SetHealthText(currentHealth, max);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gManager.OnPlayerDeath(ticket);
            OnDeath();
        }
    }

    public void OnDeath()
    {
        print("Something should have died");

        spawnManager.PlayerRebirth(this.gameObject);

        if (currentHealth <= 0 && gManager.currentTicketAmount > 0)
        {
            currentHealth = max;
        }
    }


    // currentHealth is already a public variable. No need for getter function. Also research parameters,
    // they do exactly this but in a cleaner fashion.
    //	public void GetHealth()
    //	{
    //		return currentHealth;
    //	}
    // currentHealth is already a public variable. No need for getter function. Also research parameters,
    // they do exactly this but in a cleaner fashion.
    //	public void GetHealth()
    //	{
    //		return currentHealth;
    //	}

}
