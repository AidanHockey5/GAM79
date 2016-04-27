using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

[RequireComponent(typeof(NetworkTransform))]
public class BulletController : NetworkBehaviour
{

    #region Public

    [NonSerialized] public Vector3 trailSpawnPos;
    [NonSerialized] public WeaponSettings firingWeapon;

    #endregion

    #region Private

    private BulletSettings bullet;
    private new Transform transform;
    private Vector3 posLastFrame;
    private RaycastHit rayHit;

    #endregion

    #region MonoBehaviours
    private void Start()
    {
        transform = GetComponent<Transform>();
        posLastFrame = transform.position;
        StartCoroutine(DestroyAfterTime(bullet.lifetime));
    }

    private void Update()
    {
        Move();
        CheckCollision();
    }

    #endregion

    #region Functions

    private void Move()
    {
        posLastFrame = transform.position;
        transform.position += transform.forward * bullet.speed * Time.deltaTime;
    }
    
    [Server]
    private void CheckCollision()
    {
        if (Physics.Linecast(posLastFrame, transform.position, out rayHit))
        {
            SendHit(rayHit.collider.gameObject);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    [Server]
    private void SendHit(GameObject hit)
    {
        PlayerObject playerObj;
        playerObj = hit.GetComponent<PlayerObject>();

        if (playerObj != null)
        {
            playerObj.RequestTakeDamage(GameEvent.HIT_FROM_HUMAN, firingWeapon.power);
        }
    }

    private IEnumerator DestroyAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    #endregion

}
