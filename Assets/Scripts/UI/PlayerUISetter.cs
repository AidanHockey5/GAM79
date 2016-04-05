using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerUISetter : NetworkBehaviour
{
    private string playerType;
    private GameManager gManager;
    private Health playerHealth;
    private UISetter HUDUISetter;

    [SyncVar(hook = "SetHealthBar")]public int currentHealthUI;
    [SyncVar]public int maxHealthUI;
	public GameObject localHealthDisplay;
    public Text healthText;
    public RectTransform healthBar, localHealthBar;

    // Use this for initialization
    void Awake ()
    {
        playerType = gameObject.tag;
        HUDUISetter = GameObject.Find("HUD").GetComponent<UISetter>();
        if (HUDUISetter == null)
        {
            return;
        }
        playerHealth = GetComponent<Health>();
        playerHealth.currentHealth = playerHealth.max;
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gManager == null)
        {
            return;
        }
    }
	
	// Update is called once per frame
	void Start ()
    {
        if (isLocalPlayer)
        {
            HUDEditor();
        }
    }

    void Update()
    {
        currentHealthUI = playerHealth.currentHealth;
        maxHealthUI = playerHealth.max;
        SetHealthText(currentHealthUI, maxHealthUI);
        SetHealthBar(currentHealthUI);
    }

    public void HUDEditor()
    {
        // Element 0 is Human Player's Health Text and Health Bar
        // Element 1 is Ammo
        // Element 2 is Ability 1
        // Element 3 is Ability 2
        // Element 4 is Monster Player's Health Text and Health Bar
        // Element 5 is Attack Stamina

        if (playerType == "Human" && gameObject.GetComponent<NetworkIdentity>().localPlayerAuthority == true)
        {
            for (int count = 0; count < 4; count++)
            {
                HUDUISetter.HUDStuff[count].SetActive(true);
            }
        }
        else if (playerType == "Monster" && gameObject.GetComponent<NetworkIdentity>().localPlayerAuthority == true)
        {
            for (int count = 4; count < 6; count++)
            {
                HUDUISetter.HUDStuff[count].SetActive(true);
            }
        }

		if (localHealthDisplay == null)
			return;

		localHealthDisplay.SetActive(false);                                                // Makes Local Health Bar invisible only for local player
    }

    // Sets Health Text Value for Human and Monster Players
    public void SetHealthText(int currentHealth, int maxHealth)
    {
        if (playerType == "Human")
        {
            healthText = gManager.humanHealthText;     // Utilizes the humanHealthText Text in GameManager.cs
        }
        else if (playerType == "Monster")
        {
            healthText = gManager.monsterHealthText;   // Utilizes the monsterHealthText Text in GameManager.cs
        }
        healthText.text = "Health: " + currentHealth.ToString() + "/" + maxHealth;
    }

    // Sets the HUD Health Bar and Local Health Bar Value for Human and Monster Players
    public void SetHealthBar(int health)
    {
        if (gameObject.tag == "Human")
        {
            healthBar = gManager.humanHealthBar;                                                        // Utilizes the humanHealthBar RectTransform in GameManager.cs
            healthBar.sizeDelta = new Vector2(health * 20, healthBar.sizeDelta.y);                      // rectTransform.sizeDelta.x always equals 200
            localHealthBar.sizeDelta = new Vector2(health * 20, localHealthBar.sizeDelta.y);
        }
        else if (gameObject.tag == "Monster")
        {
            healthBar = gManager.monsterHealthBar;                                                      // Utilizes the monsterHealthBar RectTransform in GameManager.cs
            healthBar.sizeDelta = new Vector2(health * 2, healthBar.sizeDelta.y);
            localHealthBar.sizeDelta = new Vector2(health * 2, localHealthBar.sizeDelta.y);
        }
    }
}
