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

	public static bool IsHost
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

	public void SetHuman()
	{
		playerPrefab = playerPrefab = spawnPrefabs[0];
		if (IsHost)
			StartHost ();
		else
			StartClient ();
			
	}

	public void SetMonser()
	{
		playerPrefab = playerPrefab = spawnPrefabs[1];
		if (IsHost)
			StartHost ();
		else
			StartClient ();
	}

	public void JoinMatch(string matchAddress)
	{
		networkAddress = matchAddress;
		MainMenu menu = GameObject.Find ("MainMenu").GetComponent<MainMenu> ();
		menu.ChangeTo (menu.playerSelectionPanel);
		//StartClient ();  --Commented out until temporary player selection is deprecated.
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
