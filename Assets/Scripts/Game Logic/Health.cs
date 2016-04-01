using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour
{
	public GameManager gManager;
	public Text healthText;
	public RectTransform healthBar, localHealthBar;
	public SpawnPointManager spawnManager;

	[SyncVar]public int max;
	[SyncVar (hook = "SetHealthBar")]public int currentHealth; // Syncs healthbar with current health value over network

	public int ticket = 1;

	private string playerType;

	void Start()
	{
		gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		spawnManager = InstanceManager.GetInstance<SpawnPointManager> ();
		playerType = gameObject.tag;

		if (gManager == null)
		{
			gManager = InstanceManager.GetInstance<GameManager> ();
		}

		if (spawnManager == null)
		{
			spawnManager = InstanceManager.GetInstance<SpawnPointManager> ();
		}

        
		this.currentHealth = this.max;
		SetHealthText(this.currentHealth, this.max);
        SetHealthBar(this.currentHealth);
	}

    void FixedUpdate()
    {
        SetHealthText(this.currentHealth, this.max);    // Used to update the status of Health Text
        SetHealthBar(this.currentHealth);               // Used to update the scale of Health Bar
    }

    public void TakeDamage(int amount)
    {
//		if (!isLocalPlayer)
//			return;
		
        currentHealth -= amount;

        this.currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gManager.OnPlayerDeath(ticket);
            OnDeath();
        }
    }

    // Sets Health Text Value for Human and Monster Players
    public void SetHealthText(int currentHealth, int maxHealth)
    {
        if (gameObject.tag == "Human")
        {
            this.healthText = gManager.humanHealthText;     // Utilizes the humanHealthText Text in GameManager.cs
        }
        else if (gameObject.tag == "Monster")
        {
            this.healthText = gManager.monsterHealthText;   // Utilizes the monsterHealthText Text in GameManager.cs
        }
        healthText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth;
    }

    // Sets the HUD Health Bar and Local Health Bar Value for Human and Monster Players
    void SetHealthBar(int health)
    {
        if (gameObject.tag == "Human")
        {
            this.healthBar = gManager.humanHealthBar;       // Utilizes the humanHealthBar RectTransform in GameManager.cs
            healthBar.GetComponent<RectTransform>().transform.localScale = new Vector2((transform.localScale.x / max) * currentHealth, transform.localScale.y);
            localHealthBar.GetComponent<RectTransform>().transform.localScale = new Vector2((transform.localScale.x / max) * currentHealth, transform.localScale.y);
        }
        else if (gameObject.tag == "Monster")
        {
            this.healthBar = gManager.monsterHealthBar;     // Utilizes the monsterHealthBar RectTransform in GameManager.cs
            healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
            localHealthBar.sizeDelta = new Vector2(health * 2, localHealthBar.sizeDelta.y);
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
