using UnityEngine;
using System.Collections;

public class SpawnPointManager : MonoBehaviour 
{
    public GameObject playerPrefab;

    public int spawnRandom;

    // Use this for initialization
   
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
       

	}

   

    public void PlayerRebirth(GameObject player)
    {
        GameManager instance = InstanceManager.GetInstance<GameManager>();

        if (currentTicketAmount <= 10 && currentTicketAmount != 0 && currentTicketAmount > -1)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            GameOver();
        }
    }
}
