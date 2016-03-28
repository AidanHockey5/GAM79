using UnityEngine;

using System.Collections;

public class SpawnPointManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject playerPrefab;

    public int spawnRandom;

    // Use this for initialization
   
	void Start () 
    {
        gameManager = InstanceManager.GetInstance<GameManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
       

	}

   

    public void PlayerRebirth(GameObject player)
    {

        if (gameManager.currentTicketAmount <= 10 && gameManager.currentTicketAmount != 0 && gameManager.currentTicketAmount > -1)
        {
            player.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            
          gameManager. GameOver();
        }
    }
}
