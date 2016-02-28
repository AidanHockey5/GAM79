using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
    [SerializeField]
    GameObject TestSpawn;
    [SerializeField]
    GameObject bottom, middle, top;
    [SerializeField]
    float sectionHight;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (TestSpawn.transform.childCount > 0)
            {
                foreach (Transform child in TestSpawn.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            GameObject CurrentLevel;
            Vector3 newPos;
            CurrentLevel = Instantiate(bottom, TestSpawn.transform.position, TestSpawn.transform.rotation) as GameObject;
            CurrentLevel.transform.parent = TestSpawn.transform;
            int rand = Random.Range(1, 8);
            int topPlace = 1;
            for (int i = 0; i < rand; i++)
            {
                newPos = new Vector3(TestSpawn.transform.position.x, TestSpawn.transform.position.y + ((i+1)*sectionHight), TestSpawn.transform.position.z);
                CurrentLevel = Instantiate(middle, newPos, TestSpawn.transform.rotation ) as GameObject;
                CurrentLevel.transform.parent = TestSpawn.transform;
                topPlace++;
            }
            newPos = new Vector3(TestSpawn.transform.position.x, TestSpawn.transform.position.y + (topPlace * sectionHight), TestSpawn.transform.position.z);
            CurrentLevel = Instantiate(top, newPos, TestSpawn.transform.rotation) as GameObject;
            CurrentLevel.transform.parent = TestSpawn.transform;
        }
	}
}
