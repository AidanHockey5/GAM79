using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CSVParser : MonoBehaviour 
{
	static string[] playerData;
	static string[] monsterData;

	[SerializeField]
	private TextAsset PlayerCSV, MonsterCSV;

	// Use this for initialization
	void Start () 
	{
		playerData = ParseData (PlayerCSV);
		monsterData = ParseData (MonsterCSV);
	
	}

	static string[] ParseData(TextAsset asset)
	{
		char[] delimiters = new char[] { ',' };
		string fileText = asset.text;
		string[] lines = fileText.Split('\n');
		List<string> strings = new List<string> ();
		for(int i = 0; i < lines.Length; i++)
		{
			string[] parts = lines[i].Split (delimiters);
			foreach (var part in parts)
			{
				strings.Add (part);
			}
		}

		return strings.ToArray();
	}
}
