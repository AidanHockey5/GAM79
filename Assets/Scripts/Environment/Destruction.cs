using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour
{
    public float minPower, maxPower;

    public GameObject[] floorStates;
    public GameObject[] middleStates;
    public GameObject[] roofStates;

    Rigidbody buildingPart;

    void Awake ()
    {
        buildingPart = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnMouseDown ()
    {
        float power = Random.Range(minPower, maxPower);
        int rand = Random.Range(0, 2);

        if (rand == 0)
            buildingPart.velocity = new Vector3(power, power, buildingPart.velocity.z);
        else if (rand == 1)
            buildingPart.velocity = new Vector3(buildingPart.velocity.x, power, power);
        else
            buildingPart.velocity = new Vector3(power, power, power);

        if (gameObject.tag == "Floor")
        {
            floorStates[0].SetActive(false);
            floorStates[1].SetActive(true);
        }
        else if (gameObject.tag == "Middle")
        {
            middleStates[0].SetActive(false);
            middleStates[1].SetActive(true);
        }
        else if (gameObject.tag == "Roof")
        {
            roofStates[0].SetActive(false);
            roofStates[1].SetActive(true);
        }
    }
}
