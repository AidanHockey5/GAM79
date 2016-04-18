using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Mine : NetworkBehaviour
{
    public float radius = 5.0f;
    public float power = 10.0f;

    [ServerCallback]
    void OnTriggerEnter(Collider other)
    {
        
    }

    [ClientRpc]
    public void RpcExplode()
    {
        /*Vector3 explosiveLocation = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosiveLocation, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosiveLocation, radius, 3.0f);
            }
        }*/

        Destroy(this.gameObject);
    }
}
