using UnityEngine;

using System.Collections;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject playerPrefab;
  
    public float positionX = 0.0f;
    public float positionZ = 0.0f;

    public bool isHitting = false;
    // Use this for initialization
   
	void Start () 
    {
        gameManager = InstanceManager.GetInstance<GameManager>();

        transform.position = new Vector3(-12, 100, 22);

        positionX = Random.Range(-100, 100);
        positionZ = Random.Range(-100, 100);
	}

	void Awake()
	{
		InstanceManager.Register (this);
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

    public void RebirthLocater()
    {
       
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, dwn, 100))
        {
            isHitting = true;
            Debug.Log("I Hit Something");
            Debug.DrawLine(transform.position, dwn, Color.cyan);

            if (isHitting == true)
            {
                RebirthLocater();
            }

        }
        transform.position = new Vector3(positionX, 100, positionZ);
        
        

    }
}
