using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

public class ProceduralCityBuild : NetworkBehaviour 
{
    public List<GameObject> buildingPrefab = new List<GameObject>();

    [SerializeField]
    float sectionHeight;

    [SerializeField]
    int xSize, Zsize, desity, minHeight, maxHeight;

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
                        int createRandom = Random.Range(1, desity);

                        if (createRandom == 3)
                        {
                            GameObject CurrentLevel;
                            Vector3 newPos = new Vector3(x, 0, z);
                            CurrentLevel = Instantiate(CustomNetworkManager.Instance.spawnPrefabs[3], newPos, Quaternion.identity) as GameObject;
							NetworkServer.Spawn (CurrentLevel);
                            int rand = Random.Range(minHeight, maxHeight);
                            int topPlace = 1;
                            for (int i = 0; i < rand; i++)
                            {
                                newPos = new Vector3(x, ((i + 1) * sectionHeight), z);
								CurrentLevel = Instantiate(CustomNetworkManager.Instance.spawnPrefabs[5], newPos, Quaternion.identity) as GameObject;
								NetworkServer.Spawn (CurrentLevel);
                               
                                topPlace++;
                            }
                            newPos = new Vector3(x, (topPlace * sectionHeight), z);
							CurrentLevel = Instantiate(CustomNetworkManager.Instance.spawnPrefabs[9], newPos, Quaternion.identity) as GameObject;
							NetworkServer.Spawn (CurrentLevel);
                           
                        }
                    }

                }
            }
        }
    }
}
