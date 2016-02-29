using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using System.Collections;

namespace UnityStandardAssets.Network
{
	public class LobbyServerList : MonoBehaviour
	{
		public RectTransform serverListRect;
		public GameObject serverEntryPrefab;
		public GameObject searchingText;

		private bool gameFound = false;

		void OnEnable()
		{
			PopulateGameList ();
		}

		void PopulateGameList()
		{
			foreach (Transform t in serverListRect)
				Destroy(t.gameObject); 

			gameFound = false;
			searchingText.SetActive (true);

			if (GetHostList () != null) 
			{
				foreach (NetworkBroadcastResult game in GetHostList().Values) 
				{
					GameObject obj = GameObject.Instantiate (serverEntryPrefab);
					obj.transform.SetParent (serverListRect.transform, false);
					string text = BytesToString (game.broadcastData);
					string[] array = text.Split (new char[] {
						':'
					});
					if (array.Length == 3 && array [0] == "NetworkManager") 
					{
						gameFound = true;
						obj.GetComponent<LobbyServerEntry> ().Populate (array [1]);
					}
				}
			} 

			if (gameFound)
				searchingText.SetActive (false);

			StartCoroutine (CheckForGames());
		}

		IEnumerator CheckForGames()
		{
			yield return new WaitForSeconds (2.0f);
			PopulateGameList ();
		}

		private string BytesToString (byte[] bytes)
		{
			char[] array = new char[bytes.Length / 2];
			Buffer.BlockCopy (bytes, 0, array, 0, bytes.Length);
			return new string (array);
		}

		public Dictionary<string, NetworkBroadcastResult> GetHostList()
		{
			if (CustomNetworkManager.Instance != null)
				return CustomNetworkManager.Instance.GetHostList();
			else
				return null;
		}
	}
}