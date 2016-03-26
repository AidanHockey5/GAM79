using System;
using UnityEngine;

public class InstanceManager 
{
	public static T GetInstance<T> () where T : class
	{
		return InstanceManager.InstanceContainer<T>.Instance as T;	
	}

	public static void Register<T> (T instance) where T : class
	{
		InstanceManager.InstanceContainer<T>.Instance = instance;
	}

	public static void Unregister<T> () where T : class
	{
		InstanceManager.InstanceContainer<T>.Instance = (T)((object)null);
	}

	private class InstanceContainer<T> where T : class
	{
		private static T _instance;
		private static 

		public static T Instance
		{
			get 
			{
				T instance = InstanceManager.InstanceContainer<T>._instance;
				T result;
				if (instance != null) 
				{
					result = instance;
				}
				else 
				{
					result = (InstanceManager.InstanceContainer<T>._instance = Activator.CreateInstance<T>());
				}
				return result;
			}
			set
			{
				InstanceManager.InstanceContainer<T>._instance = value;
			}
		}
	}
}