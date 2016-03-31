using UnityEngine;

using System.Collections;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    GameManager gameManager;
    MonsterDistance monstDistance;
    SpawnPoints spawnPoint;

    public GameObject player;
     
    public float positionX = 0.0f;
    public float positionZ = 0.0f;
    public float distance = 0.0f;
    public float monsterDistance = 0.0f;
 
   
    // Use this for initialization
   
	void Start () 
    {
        gameManager = InstanceManager.GetInstance<GameManager>();
        monstDistance = InstanceManager.GetInstance<MonsterDistance>();
        
        transform.position = new Vector3(-12, 100, 22);
	}

	void Awake()
	{
		InstanceManager.Register (this);
	}
    void Update()
    {
       //monsterDistance = Vector3.Distance(gameObject.GetComponent<MonsterDistance>().transform.position, player.transform.position);
        if (spawnPoint.isHitting == true)
        {
            PlayerRebirth(player);
        }
        
    }
	// Update is called once per frame
	
 
    public void PlayerRebirth(GameObject player)
    {

        if (gameManager.currentTicketAmount <= 10 && gameManager.currentTicketAmount != 0 && gameManager.currentTicketAmount > -1 && spawnPoint.isHitting == true)
        {       
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
           
            if (distance <= 100f && monsterDistance >= 100f  || monsterDistance < 150f )
            {
                player.transform.position = new Vector3(positionX, 0, positionZ);
            }            
    }
}
