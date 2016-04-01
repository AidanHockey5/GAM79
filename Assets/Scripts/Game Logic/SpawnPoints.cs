using UnityEngine;
using System.Collections;


public class SpawnPoints : MonoBehaviour 
{
    MonsterDistance monstDistance;
    SpawnPointManager spawnManager;

    public GameObject player;

    public float distance = 0.0f;
    public float monsterDistance = 0.0f;
    public float spawnCounter = 0.0f;

    public bool isHitting = false;
	// Use this for initialization
	void Start () 
    {
        InstanceManager.GetInstance<SpawnPointManager>().RegisterSpawnPoint(this);
	}
   
	// Update is called once per frame
    void Update()
    {
        print("I am Being Called");
        if (spawnManager.gameObject.active == true)
        {
            Vector3 dwn = transform.TransformDirection(Vector3.down);

            if (Physics.Raycast(transform.position, dwn, 100))
            {
                Debug.Log("I Hit Something");
                Debug.DrawLine(transform.position, dwn, Color.cyan);
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                distance = hit.distance;
                if (distance >= 100f)
                {
                    isHitting = true;
                }

            }
        }
        if (spawnManager.gameObject.active == false)
        {
            isHitting = false;
            if (isHitting == false)
            {
                spawnManager.PlayerRebirth(player);
            }
        }
    
    } 
}
