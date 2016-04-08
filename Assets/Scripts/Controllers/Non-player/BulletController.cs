using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(NetworkTransform))]
public class BulletController : NetworkBehaviour
{

    public float speed;
    public float lifetime;
    public WeaponSettings firingWeapon;

    new Transform transform;
    Vector3 posLastFrame;
    RaycastHit rayHit;

    [ServerCallback]
    void Start()
    {
        transform = GetComponent<Transform>();
        posLastFrame = transform.position;
        StartCoroutine(DeadAfterTime(lifetime));
    }
    
    [ServerCallback]
    void Update()
    {
        Move();
        CheckCollision();
    }

    [Server]
    void Move()
    {
        posLastFrame = transform.position;
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    [Server]
    void CheckCollision()
    {
        if (Physics.Linecast(posLastFrame, transform.position, out rayHit))
        {
            //RpcAnnounceHit();
            SendHit(rayHit.collider.gameObject);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
    
    [Server]
    void SendHit(GameObject hit)
    {
        PlayerObject playerObj;
        playerObj = hit.GetComponent<PlayerObject>();

        if (playerObj != null)
        {
            playerObj.TakeDamage(GameEvent.HIT_FROM_HUMAN, firingWeapon.power);
        }
    }

    IEnumerator DeadAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
