using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

public class ProceduralCityBuild : NetworkBehaviour 
{
	
    public List<GameObject> buildingPrefab = new List<GameObject>();
	private Dictionary<int, Dictionary<GameObject, Vector3>> buildings = new Dictionary<int, Dictionary<GameObject, Vector3>>();
	int count = 0;

    [SerializeField]
    float sectionHeight;

    [SerializeField]
    int xSize, Zsize;

	[ServerCallback]
    void Start () 
	{
		Build ();
	}
		
	[Server]
    void Build()
    {
       for(int x = -xSize; x < xSize; x++)
        {
            if (x % 20 == 0)
            {
                for (int z = -Zsize; z < Zsize; z++)
                {
                    if(z % 20 == 0)
                    {
                        int createRandom = Random.Range(1, 5);

                        if (createRandom == 3)
                        {
                            GameObject CurrentLevel;
                            Vector3 newPos = new Vector3(x, 0, z);
                            CurrentLevel = Instantiate(buildingPrefab[0], newPos, Quaternion.identity) as GameObject;
							NetworkServer.Spawn (CurrentLevel);
                            int rand = Random.Range(1, 8);
                            int topPlace = 1;
                            for (int i = 0; i < rand; i++)
                            {
                                newPos = new Vector3(x, ((i + 1) * sectionHeight), z);
                                CurrentLevel = Instantiate(buildingPrefab[1], newPos, Quaternion.identity) as GameObject;
								NetworkServer.Spawn (CurrentLevel);
                               
                                topPlace++;
                            }
                            newPos = new Vector3(x, (topPlace * sectionHeight), z);
                            CurrentLevel = Instantiate(buildingPrefab[2], newPos, Quaternion.identity) as GameObject;
							NetworkServer.Spawn (CurrentLevel);
                           
                        }
                    }

                }
            }
        }
    }
}
