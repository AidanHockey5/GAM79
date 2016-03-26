using UnityEngine;
using System.Collections;

public class MonsterAbilityManager : MonoBehaviour {

    [SerializeField] GameObject[] attackAreas;


    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void ActivateAtackArea(int attackAreaIndex)
    {
        attackAreas[attackAreaIndex].SetActive(true);
    }

    public void DeactivateAtackArea(int attackAreaIndex)
    {
        attackAreas[attackAreaIndex].SetActive(false);
    }
}
