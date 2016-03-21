﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class BaseWeaponController : NetworkBehaviour
{
	public WeaponSettings[] equippableWeapons;
	public bool isReloading, canFire;
	public Transform playerCam;
	public string[] attackableTargets;

	private WeaponSettings m_currentWeapon;

	RaycastHit rayHit;
	bool targetHit;

	// Use this for initialization
	void Start () 
	{
       
	}

	void SetWeapon(int slot)
	{
		m_currentWeapon = equippableWeapons[slot];
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
								// targetHealth.TakeDamage(power);
							}
						}
					}
				}

				if (m_currentWeapon.currentAmmo <= 0)
				{
					isReloading = true;
					StartCoroutine(StartReloadTimer(m_currentWeapon.reloadTime));
				}
				else
				{
					StartCoroutine(StartAttackTimer(m_currentWeapon.attackRate));
				}
			}
		}
	}

	public virtual void ReloadWeapon()
	{
		m_currentWeapon.currentAmmo = m_currentWeapon.maxAmmo;
	}

	IEnumerator StartAttackTimer(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		m_currentWeapon.currentAmmo--;
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
