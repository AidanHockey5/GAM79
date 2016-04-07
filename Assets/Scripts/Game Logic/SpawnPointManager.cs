using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Singleton]
public class SpawnPointManager : MonoBehaviour
{
   public SpawnPoints spawn;
   public MonsterDistance monstDist;
   public BaseCharacterController player;

   public GameObject target;

    public Vector3 playerPosition;
    public Vector3 pointPosition;

    public float pointDistance = 0.0f;
   

    void Awake()
    {
        InstanceManager.Register(this);
    }
    // Use this for initialization
   
	void Start () 
    {
       
       
        
	}
    void Update()
    {
        player = GameObject.FindObjectOfType<BaseCharacterController>();
    }

    public void PlayerRebirth(GameObject player)
     {
         playerPosition = player.transform.position;
        
         PointLocation(playerPosition, 150f);
   }

   public  void PointLocation(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        int i = 0;
        spawn = GameObject.FindObjectOfType<SpawnPoints>();

        if (hitColliders[i] != spawn)
        {
            i++;

            while (i < hitColliders.Length)
            {

                spawn = GameObject.FindObjectOfType<SpawnPoints>();
                monstDist = GameObject.FindObjectOfType<MonsterDistance>();
                print("solo");
                pointDistance = Vector3.Distance(hitColliders[i].transform.position, monstDist.transform.position);
                pointPosition = hitColliders[i].transform.position;
                print("yo");

                if (pointDistance <= 150f || pointDistance >= 300f)
                {
                    int pointLocation = Random.Range(0, 4);
                    print("im here");
                    if (pointLocation >= 0)
                    {
                        print("Hello");
                        player.transform.position = pointPosition;
                    }
                }

                i++;
            }

        }
    }
}
