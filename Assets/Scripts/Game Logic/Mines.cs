using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(NetworkTransform))]
public class Mines : NetworkBehaviour
{
    public GameObject minePrefab;
    // Use this for initialization
    [ServerCallback]
	void Start () 
    {
        if (minePrefab == null)
        {
            minePrefab = (GameObject)(UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Mine.fbx", typeof(GameObject)));
        }
	}
	
	// Update is called once per frame
   [Server]
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           MineCreated();
        }
	}
    [Server]
    public void MineCreated()
    {
        Instantiate(minePrefab, transform.position, Quaternion.identity);
        
    }
   
}
