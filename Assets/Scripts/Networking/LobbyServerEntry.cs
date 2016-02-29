using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;

namespace UnityStandardAssets.Network
{
	public class LobbyServerEntry : MonoBehaviour 
	{
		public Text serverInfoText;
		public Button joinButton;

		private string myNetworkAddress;

		public void Populate(string networkAddress)
		{
			serverInfoText.text = networkAddress;
			myNetworkAddress = networkAddress;
		}

		public void JoinMatch()
		{
			if(NetworkServer.connections.Count < CustomNetworkManager.Instance.maxConnections)
				CustomNetworkManager.Instance.JoinMatch (myNetworkAddress);
		}
	}
}