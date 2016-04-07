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

    void Start()
    {
        transform = GetComponent<Transform>();
        posLastFrame = transform.position;
        StartCoroutine(DeadAfterTime(lifetime));
    }

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority)
            return;

        CmdApplyThrust();
        CmdCheckCollision();
        posLastFrame = transform.position;
    }

    [Command]
    void CmdApplyThrust()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    [Command]
    void CmdCheckCollision()
    {
        if (Physics.Linecast(posLastFrame, transform.position, out rayHit))
        {
            CmdSendHit(rayHit.collider.gameObject);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    [Command]
    void CmdSendHit(GameObject hit)
    {
        hit.SendMessage(MessageSettings.TAKE_DAMAGE, firingWeapon.power, SendMessageOptions.RequireReceiver);
    }

    IEnumerator DeadAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
