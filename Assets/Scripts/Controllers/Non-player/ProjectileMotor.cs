using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ProjectileMotor : NetworkBehaviour
{
	public float speed;
	public float lifetime;
	public BaseWeaponController controller;
    public WeaponSettings firingWeapon;

	new Transform transform;
	Vector3 posLastFrame;
	RaycastHit rayHit;

	// Use this for initialization
	void Start () 
	{
		transform = GetComponent<Transform> ();
		posLastFrame = transform.position;
		StartCoroutine (DeadAfterTime (lifetime));
	}
	
	// Update is called once per frame
	[ClientCallback]
	void Update () 
	{
        if (!isLocalPlayer)
            return;

		ApplyThrust ();
		CheckCollision ();
		posLastFrame = transform.position;
	}

	void ApplyThrust()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	void CheckCollision()
	{
		if (Physics.Linecast(posLastFrame, transform.position, out rayHit))
		{
            CmdSendDamage(firingWeapon.power, rayHit.collider.gameObject);
		}
	}

    [Command]
    void CmdSendDamage(float amount, GameObject hit)
    {

    }

	IEnumerator DeadAfterTime(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		Destroy (gameObject);
	}
}
