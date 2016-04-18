using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPointManager : MonoBehaviour
{
	public List<SpawnPoints> spawnPoints = new List<SpawnPoints>();

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

	public void RegisterSpawnPoint(SpawnPoints point)
	{
		spawnPoints.Add (point);
	}

	public Vector3 SpawnPointLocation()
	{
		int pointLocation = Random.Range(0, (spawnPoints.Count -1));

		for (int i = 0; i < spawnPoints.Count; i++)
		{
			if (i == pointLocation)
				return spawnPoints [i].transform.position;
		}

		return Vector3.zero;
	}
}
