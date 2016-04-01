using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LocalUIDisplay : NetworkBehaviour
{
    public GameObject localHealthDisplay;

	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (localHealthDisplay == null)
			return;
		
        if (gameObject.tag == "HumanHP" && gameObject.GetComponentInParent<NetworkIdentity>().isLocalPlayer)
        {
            localHealthDisplay.SetActive(false);                                                // Makes Local Health Bar invisible only for local player
            if (GameObject.FindGameObjectWithTag("Monster") == null)
            {
                return;
            }
            transform.LookAt(GameObject.FindGameObjectWithTag("Monster").transform.position);   // Will Find Monster and other Human Cameras Later
        }
        else if (gameObject.tag == "MonsterHP" && gameObject.GetComponentInParent<NetworkIdentity>().isLocalPlayer)
        {
            localHealthDisplay.SetActive(false);
            if (GameObject.FindGameObjectWithTag("Human") == null)
            {
                return;
            }
            transform.LookAt(GameObject.FindGameObjectWithTag("Human").transform.position);
        }
    }
}
