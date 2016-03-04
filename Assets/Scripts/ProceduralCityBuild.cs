using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralCityBuild : MonoBehaviour 
{
    public List<GameObject> buildingPrefab = new List<GameObject>();
    [SerializeField]
    float sectionHeight;

    // Use this for initialization
    void Start () 
    {
        Build();
	}
	
	// Update is called once per frame
	void Update () 
    {
    
	}

    void Build()
    {
       for(int x = -1000; x < 1000; x++)
        {
            if (x % 20 == 0)
            {
                for (int z = -1000; z < 1000; z++)
                {
                    if(z % 20 == 0)
                    {
                        int createRandom = Random.Range(1, 5);

                        if (createRandom == 3)
                        {
                            GameObject CurrentLevel;
                            Vector3 newPos = new Vector3(x, 0, z);
                            CurrentLevel = Instantiate(buildingPrefab[0], newPos, Quaternion.identity) as GameObject;
                           
                            int rand = Random.Range(1, 8);
                            int topPlace = 1;
                            for (int i = 0; i < rand; i++)
                            {
                                newPos = new Vector3(x, ((i + 1) * sectionHeight), z);
                                CurrentLevel = Instantiate(buildingPrefab[1], newPos, Quaternion.identity) as GameObject;
                               
                                topPlace++;
                            }
                            newPos = new Vector3(x, (topPlace * sectionHeight), z);
                            CurrentLevel = Instantiate(buildingPrefab[2], newPos, Quaternion.identity) as GameObject;
                           
                        }
                    }

                }
            }
        }
    }
}
