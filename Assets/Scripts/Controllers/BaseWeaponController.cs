using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class BaseWeaponController : NetworkBehaviour
{
	public float attackRate, reloadTime, range;
	public int power, maxAmmo, currentAmmo;
	public bool isReloading, canFire;
	public Transform playerCam;
	public string[] attackableTargets;

   

	RaycastHit rayHit;
	bool targetHit;

	// Use this for initialization
	void Start () 
	{
       
	}

	void GetInput()
	{
		if (Input.GetMouseButton(0))
		{
			FireWeapon();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadWeapon();
		}
	}
	
	// Update is called once per frame
	[ClientCallback]
	void Update () 
	{
		if (!isLocalPlayer)
			return;

		GetInput();
	}

	public virtual void FireWeapon()
	{
		if (!isReloading)
		{
			// shoot
			if (canFire)
			{
				targetHit = Physics.Raycast(playerCam.position, playerCam.forward, out rayHit);
				canFire = false;
				
				if (targetHit)
				{
					for (int i = 0; i < attackableTargets.Length; i++)
					{
						if (rayHit.transform.tag == attackableTargets[i])
						{
							Health targetHealth = rayHit.transform.GetComponent<Health>();

							if (targetHealth != null)
							{
								targetHealth.TakeDamage(power);
							}
						}
					}
				}

				if (currentAmmo <= 0)
				{
					isReloading = true;
					StartCoroutine(StartReloadTimer(reloadTime));
				}
				else
				{
					StartCoroutine(StartAttackTimer(attackRate));
				}
			}
		}
	}

	public virtual void ReloadWeapon()
	{
		currentAmmo = maxAmmo;
	}

	IEnumerator StartAttackTimer(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		currentAmmo--;
		canFire = true;
	}

	IEnumerator StartReloadTimer(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ReloadWeapon();
		canFire = true;
		isReloading = false;
	}


}
