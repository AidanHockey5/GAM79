using UnityEngine;
using System.Collections.Generic;
using System;

public class SingletonCreator : MonoBehaviour 
{
	void Awake () 
	{
		CreateSingletons ();
	}
	
	private void CreateSingletons()
	{
		var singletonTypes = new List<Type>();
		foreach(var candidate in GetType().Assembly.GetExportedTypes())
		{
			var attributes = candidate.GetCustomAttributes(typeof(Singleton), true);
			if (attributes != null && attributes.Length > 0)
			{
				singletonTypes.Add(candidate);
			}
		}
		new GameObject("Singletons", singletonTypes.ToArray());
	}
}
