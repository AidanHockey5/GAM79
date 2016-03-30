using UnityEngine;

using System.Collections;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    GameManager gameManager;
    MonsterDistance monstDistance;

    public GameObject player;
  
    public float positionX = 0.0f;
    public float positionZ = 0.0f;
    public float distance = 0.0f;
    public float monsterDistance = 0.0f;

    public bool isHitting = false;
    // Use this for initialization
   
	void Start () 
    {
        gameManager = InstanceManager.GetInstance<GameManager>();
        monstDistance = InstanceManager.GetInstance<MonsterDistance>();

        monsterDistance = Vector3.Distance(monstDistance.transform.position, player.transform.position);

        transform.position = new Vector3(-12, 100, 22);
	}

	void Awake()
	{
		InstanceManager.Register (this);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            distance =  hit.distance;
            if (distance <= 100f)
            {
                isHitting = true;
            }
        }

	}
 
    public void PlayerRebirth(GameObject player)
    {

        if (gameManager.currentTicketAmount <= 10 && gameManager.currentTicketAmount != 0 && gameManager.currentTicketAmount > -1)
        {
            player.transform.position = new Vector3(0, 0, 0);
            RebirthLocater( player);
        }
        else
        {
            
          gameManager. GameOver();
        }
    }            

    public void RebirthLocater(GameObject player)
    {
        positionX = Random.Range(-100, 100);
        positionZ = Random.Range(-100, 100);

        Vector3 dwn = transform.TransformDirection(Vector3.down);

     
        if (Physics.Raycast(transform.position, dwn, 100))
        {
            Debug.Log("I Hit Something");
            Debug.DrawLine(transform.position, dwn, Color.cyan);

            if (distance <= 100f && monsterDistance <= 20f && isHitting == true)
            {
                player.transform.position = new Vector3(positionX, 100, positionZ);
            }
        }
       
        
        

    }
}
