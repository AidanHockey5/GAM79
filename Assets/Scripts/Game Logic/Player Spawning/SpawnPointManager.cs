using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPointManager : MonoBehaviour
{
    public Dictionary<int, SpawnPoints> spawnPoints = new Dictionary<int, SpawnPoints>();

	private static SpawnPointManager instance_ = null;

	public static SpawnPointManager Instance
	{
		get
		{
			if (instance_ != null)
			{
				return instance_;
			}
			else
			{
				GameObject go = new GameObject();
				return instance_ = go.AddComponent<SpawnPointManager>();
			}
		}
	}

	void Awake()
	{
		instance_ = this;
	}

	public void RegisterSpawnPoint(int key, SpawnPoints point)
	{
		spawnPoints.Add (key, point);
	}

    public Vector3  SpawnPointLocation(int localPoint)
    {
        return spawnPoints[localPoint].transform.position;
    }
}
