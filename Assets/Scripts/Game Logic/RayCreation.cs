using UnityEngine;
using System.Collections;

public class RayCreation : MonoBehaviour 
{
    SpawnPointManager spawnManager;

    public float distance = 0.0f;

    public bool isHitting = true;
	// Use this for initialization
	void Start () 
    {
        InstanceManager.GetInstance<SpawnPointManager>().RegisterRayCreation(this);
        spawnManager = InstanceManager.GetInstance<SpawnPointManager>();
	}
	
	// Update is called once per frame
	void Update () 
    {
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
            if (isHitting == true)
            {
                this.gameObject.active = true;
            }
            else
            {
                this.gameObject.active = false;
            }
        }
	}
}
