using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
	public GameManager gManager;
	public int max;
	public int currentHealth;

    public int ticket = 1;

	void Start()
	{
		gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

		if (gManager == null)
		{
			gameObject.SetActive(false);
			Debug.LogError("There is no active GameManager in scene.");
		}
		else
		{
			currentHealth = max;
			gManager.SetHealthText(currentHealth);
		}
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		gManager.SetHealthText(currentHealth);

        if (currentHealth <= 0)
        {
            gManager.OnPlayerDeath(ticket);
            OnDeath();
        }
	}

    public void OnDeath()
    {
        print("Something should have died");
        Destroy(this.gameObject);
        gManager.GameOver();
    }

    
	// currentHealth is already a public variable. No need for getter function. Also research parameters,
	// they do exactly this but in a cleaner fashion.
//	public void GetHealth()
//	{
//		return currentHealth;
//	}

}
