using UnityEngine;
using System.Collections;

public class Scatter : MonoBehaviour {

    [SerializeField]
    float minPower, maxPower;

    // Use this for initialization
    void Start ()
    {
        if (minPower == 0)
        {
            minPower = 0;
        }
        if (maxPower == 0)
        {
            maxPower = 0;
        }

        float powerx = Random.Range(minPower, maxPower);
        float powery = Random.Range(-minPower, -maxPower);
        float powerz = Random.Range(minPower, maxPower);

        this.GetComponent<Rigidbody>().AddForce(new Vector3(powerx, powery, powerz));

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
