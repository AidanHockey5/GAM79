using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Mine : NetworkBehaviour
{
    public float radius = 5.0f;
    public int damage = 100;

    void OnTriggerEnter(Collider other)
    {
        PlayerObject po = other.gameObject.GetComponent<PlayerObject>();

        if (po.playerSettings.playerType == PlayerType.MONSTER)
        {
            po.RequestTakeDamage(GameEvent.HIT_FROM_HUMAN, damage);
            Destroy(gameObject);
        }
        else if (po == null)
        {
            Debug.LogError("No PlayerObject attached to other collider.");
        }
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
    }
}
