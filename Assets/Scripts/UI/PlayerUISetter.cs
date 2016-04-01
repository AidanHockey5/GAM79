using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerUISetter : NetworkBehaviour
{
    private string playerType;
    private UISetter HUDUISetter;

	// Use this for initialization
	void Awake ()
    {
        playerType = gameObject.tag;
        HUDUISetter = GameObject.Find("HUD").GetComponent<UISetter>();
	}
	
	// Update is called once per frame
	void Start ()
    {
        if (isLocalPlayer)
        {
            HUDEditor();
        }
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
    }
}
