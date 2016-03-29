using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class UISetter : NetworkBehaviour
{
    public GameObject[] HUDStuff;

    private GameObject human, monster;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //HUDEditor();
    }

    public void HUDEditor()
    {
        human = GameObject.FindGameObjectWithTag("Human");
        monster = GameObject.FindGameObjectWithTag("Monster");

        // Element 0 is Ammo
        // Element 1 is Ability 1
        // Element 2 is Ability 2
        // Element 3 is Attack Stamina

        if (human && human.GetComponent<NetworkIdentity>().localPlayerAuthority == true)
        {
            for (int count = 0; count < 3; count++)
            {
                HUDStuff[count].SetActive(true);
            }
        }
        else if (monster && monster.GetComponent<NetworkIdentity>().localPlayerAuthority == true)
        {
            HUDStuff[3].SetActive(true);
        }
    }
}
