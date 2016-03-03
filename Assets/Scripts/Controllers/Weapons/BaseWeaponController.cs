using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BaseWeaponController : NetworkBehaviour
{
	public float attackRate, reloadTime;
	public int weaponPower, maxAmmo, currentAmmo;
	public bool isReloading, canFire;

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
				Debug.Log ("FIRE");
				canFire = false;

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
		Debug.Log ("RELOAD");
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
