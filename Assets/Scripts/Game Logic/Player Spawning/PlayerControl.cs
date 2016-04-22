using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerControl : NetworkBehaviour 
{
	private SpawnClass _currentClass = global::SpawnClass.None;

	void Start()
	{
		if (isLocalPlayer)
		{
			GameManager.Instance.RegisterPlayer (this);
		}
	}

	public void SpawnClass(SpawnClass sClass)
	{
		if (_currentClass != sClass)
			CmdSpawn (sClass);
		else
			this.transform.position = SpawnPointManager.Instance.SpawnPointLocation (1);

		GameManager.Instance.OnCharacterSpawn (sClass);
	}

	[Command]
	public void CmdSpawn(SpawnClass sClass)
	{
		_currentClass = sClass;
		switch (sClass)
		{
		case global::SpawnClass.Technician:
			GameObject obj = (GameObject)Instantiate (CustomNetworkManager.Instance.spawnPrefabs [3], SpawnPointManager.Instance.SpawnPointLocation (1), Quaternion.identity);
			NetworkServer.Destroy (this.gameObject);
			NetworkServer.ReplacePlayerForConnection (this.connectionToClient, obj, this.playerControllerId);
			break;
		case global::SpawnClass.Support:
			GameObject obj2 = (GameObject)Instantiate (CustomNetworkManager.Instance.spawnPrefabs [0], SpawnPointManager.Instance.SpawnPointLocation (1), Quaternion.identity);
			NetworkServer.Destroy (this.gameObject);
			NetworkServer.ReplacePlayerForConnection (this.connectionToClient, obj2, this.playerControllerId);
			break;
		case global::SpawnClass.Heavy:
			GameObject obj3 = (GameObject)Instantiate (CustomNetworkManager.Instance.spawnPrefabs [4], SpawnPointManager.Instance.SpawnPointLocation (1), Quaternion.identity);
			NetworkServer.Destroy (this.gameObject);
			NetworkServer.ReplacePlayerForConnection (this.connectionToClient, obj3, this.playerControllerId);
			break;
		case global::SpawnClass.Assault:
			GameObject obj4 = (GameObject)Instantiate (CustomNetworkManager.Instance.spawnPrefabs [5], SpawnPointManager.Instance.SpawnPointLocation (1), Quaternion.identity);
			NetworkServer.Destroy (this.gameObject);
			NetworkServer.ReplacePlayerForConnection (this.connectionToClient, obj4, this.playerControllerId);
			break;
		}
	}
}

