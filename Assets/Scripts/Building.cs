using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
	#region OLD CODE
//    [SerializeField]
//    GameObject TestSpawn;
//    [SerializeField]
//    GameObject bottom, middle, top;
//    [SerializeField]
//    float sectionHight;
	#endregion
	 public GameObject brokenVer = null;


	void Start ()
    {
	
	}

	void Update ()
    {
		#region OLD CODE
//        if (Input.GetKeyDown(KeyCode.A))
//        {
//            if (TestSpawn.transform.childCount > 0)
//            {
//                foreach (Transform child in TestSpawn.transform)
//                {
//                    Destroy(child.gameObject);
//                }
//            }
//            GameObject CurrentLevel;
//            Vector3 newPos;
//            CurrentLevel = Instantiate(bottom, TestSpawn.transform.position, TestSpawn.transform.rotation) as GameObject;
//            CurrentLevel.transform.parent = TestSpawn.transform;
//            int rand = Random.Range(1, 8);
//            int topPlace = 1;
//            for (int i = 0; i < rand; i++)
//            {
//                newPos = new Vector3(TestSpawn.transform.position.x, TestSpawn.transform.position.y + ((i+1)*sectionHight), TestSpawn.transform.position.z);
//                CurrentLevel = Instantiate(middle, newPos, TestSpawn.transform.rotation ) as GameObject;
//                CurrentLevel.transform.parent = TestSpawn.transform;
//                topPlace++;
//            }
//            newPos = new Vector3(TestSpawn.transform.position.x, TestSpawn.transform.position.y + (topPlace * sectionHight), TestSpawn.transform.position.z);
//            CurrentLevel = Instantiate(top, newPos, TestSpawn.transform.rotation) as GameObject;
//            CurrentLevel.transform.parent = TestSpawn.transform;
//        }
		#endregion
	}

	public void PrepareDestruction(Vector3 direction)
	{
		DestructionManager.instance.DestroyObject (gameObject, brokenVer, direction);
	}

	void OnTriggerEnter (Collider other)
	{
//		if (other.gameObject.tag == "broken") 
//		{
//			if (other.GetComponent<Rigidbody>() != null) 
//			{
//				DestructionManager.instance.DestroyObject (gameObject, brokenVer, other.transform.forward);
////				float speed = other.GetComponent<Rigidbody> ().velocity.magnitude;
////				if (speed >= 20f) 
////				{
////					print ("I SHOULD FUCKING EXPLODE");
////
////				}
//			}
//		}
	}
}
