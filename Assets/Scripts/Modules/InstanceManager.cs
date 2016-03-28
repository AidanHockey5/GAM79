using System;
using UnityEngine;
using System.Collections.Generic;

public class InstanceManager : MonoBehaviour
{
	private static Dictionary<Type, MonoBehaviour> _instances = new Dictionary<Type, MonoBehaviour>();

	public static T GetInstance<T>() where T : MonoBehaviour
	{
		MonoBehaviour instance;
		_instances.TryGetValue (typeof(T), out instance);
		return instance as T;
	}

	public static void Register<T>(T instance) where T : MonoBehaviour
	{
		_instances.Add(typeof(T), instance);
	}
}