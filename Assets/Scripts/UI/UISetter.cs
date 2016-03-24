using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class UISetter : MonoBehaviour
{
    public GameObject[] HUDStuff;

    private GameObject human, monster;

	// Use this for initialization
	void Start ()
    {
        HUDEditor();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
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
        else
        {
            for (int count = 0; count < 3; count++)
            {
                HUDStuff[count].SetActive(false);
            }
        }
        if (monster && monster.GetComponent<NetworkIdentity>().localPlayerAuthority == true)
        {
            HUDStuff[3].SetActive(true);
        }
        else
        {
            HUDStuff[3].SetActive(false);
        }
    }
}
