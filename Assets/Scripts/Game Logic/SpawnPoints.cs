using UnityEngine;
using System.Collections;

public class SpawnPoints : MonoBehaviour 
{
    public GameObject player;

    public float distance = 0.0f;
    public float monsterDistance = 0.0f;
    public float spawnCounter = 0.0f;

    public bool isHitting = false;
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        monsterDistance = Vector3.Distance(gameObject.GetComponent<MonsterDistance>().transform.position, player.transform.position);
	}

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 dwn = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, dwn, 100))
        {
            Debug.Log("I Hit Something");
            Debug.DrawLine(transform.position, dwn, Color.cyan);
        }
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            distance = hit.distance;
            if (distance >= 100f)
            {
                isHitting = true;
            }
           
            if (isHitting == false && monsterDistance < 150f)
            {
                gameObject.active = false;
            }
        }
       
        gameObject.active = true;
    }
}
