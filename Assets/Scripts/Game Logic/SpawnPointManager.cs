﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
    GameManager gameManager;
    MonsterDistance monstDistance;

    public List<SpawnPoints> spawnPointObject = new List<SpawnPoints>();

    public GameObject player;
     
    public float positionX = 0.0f;
    public float positionZ = 0.0f;
    public float monsterDistance = 0.0f;

    void Awake()
    {
        InstanceManager.Register(this);
    }
    // Use this for initialization
   
	void Start () 
    {
        gameManager = InstanceManager.GetInstance<GameManager>();
        monstDistance = InstanceManager.GetInstance<MonsterDistance>();
        
        transform.position = new Vector3(-12, 100, 22);

        
	}
    void Update()
    {
        
            foreach (SpawnPoints item in spawnPointObject)
            {
                print("i see you");
                if (item != null)
                {
                    gameObject.active = true;
                   
                }
                if (item == null)
                {
                    print("Were did you go");
                    gameObject.active = false;

                }
            }
        
    }
	// Update is called once per frame
   
    public void RegisterSpawnPoint(SpawnPoints point)
    {
        spawnPointObject.Add(point);        
    }
    public void RegisterMonsterDistance()
    {
        monsterDistance = Vector3.Distance(gameObject.GetComponent<MonsterDistance>().transform.position, player.transform.position);
    }

    public void PlayerRebirth(GameObject player)
    {

        if (gameManager.currentTicketAmount <= 10 && gameManager.currentTicketAmount != 0 && gameManager.currentTicketAmount > -1 )
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
        //this needs to change
        positionX = Random.Range(-100, 100);
        positionZ = Random.Range(-100, 100);   
           
            if (monsterDistance >= 100f  || monsterDistance < 150f )
            {
                player.transform.position = new Vector3(positionX, 0, positionZ);
            }            
    }
}