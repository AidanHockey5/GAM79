using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;

public class CustomNetworkManager : NetworkManager 
{
	public NetworkDiscovery discovery;

	private static CustomNetworkManager _instance;
	private static bool _isHost = false;

	public static CustomNetworkManager Instance
	{
		get { return _instance; }
		private set { _instance = value; }
	}

	public bool IsHost
	{
		get { return _isHost; }
		private set { _isHost = value; }
	}

	void Start()
	{
		Instance = this;
	}

	public override void OnStartHost ()
	{
		base.OnStartHost ();

		IsHost = true;
		StartBroadcasting ();
	}


	public void SetMonser()
	{
		playerPrefab = spawnPrefabs[1];
		if (IsHost)
			StartHost ();
		else
			StartClient ();
	}

	public void SetTechnician()
	{
		playerPrefab = spawnPrefabs[3];
		if (IsHost)
			StartHost ();
		else
			StartClient ();
	}

	public void SetSupport()
	{
		playerPrefab = spawnPrefabs[0];
		if (IsHost)
			StartHost ();
		else
			StartClient ();
	}

	public void SetHeavy()
	{
		playerPrefab = spawnPrefabs[4];
		if (IsHost)
			StartHost ();
		else
			StartClient ();
	}

	public void SetAssault()
	{
		playerPrefab = spawnPrefabs[5];
		if (IsHost)
			StartHost ();
		else
			StartClient ();
	}

	public void JoinMatch(string matchAddress)
	{
		networkAddress = matchAddress;
		StartClient ();
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		if (NetworkServer.connections.Count > 1)
			playerPrefab = spawnPrefabs [0];

		base.OnServerAddPlayer (conn, playerControllerId);
	}

	public void StartBroadcasting()
	{
		discovery.Initialize();
		discovery.StartAsServer ();
	}

	public void StartListening()
	{
		discovery.Initialize();
		discovery.StartAsClient ();
	}

	public void StopBroadcasting()
	{
		if (discovery.isClient || discovery.isServer)
			return;

		discovery.StopBroadcast ();
	}

	public Dictionary<string, NetworkBroadcastResult> GetHostList()
	{
		return discovery.broadcastsReceived;
	}

	public void StartHosting()
	{
		IsHost = true;
		networkAddress = LocalIP ();
		//StartHost ();
	}

	public string LocalIP()
	{
		IPHostEntry host;
		string localIP = "";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
				break;
			}
		}
		return localIP;
	}
}
