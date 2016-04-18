﻿using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using System.Collections;


public class BaseWeaponController : NetworkBehaviour
{
    public AudioClip gunShot = null;
    public AudioMixerGroup gunShots = null;
    
    public WeaponSettings[] equippableWeapons;
	public bool isReloading, canFire;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
    public Transform trailSpawn;

	private WeaponSettings m_currentWeapon;

	RaycastHit rayHit;
	bool targetHit;

	// Use this for initialization
	void Start () 
	{
		SetWeapon (0);
	}

	void SetWeapon(int slot)
	{
		m_currentWeapon = equippableWeapons[slot];
	}

	void GetInput()
	{
		if (Input.GetMouseButton(0))
		{
			CmdFireWeapon();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			CmdReloadWeapon();
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

    [Command]
	public void CmdFireWeapon()
	{
		if (!isReloading)
		{
			if (canFire)
			{
				GameObject lastBulletFired = (GameObject) Instantiate (CustomNetworkManager.Instance.spawnPrefabs[2], bulletSpawn.position, bulletSpawn.rotation);
                BulletController bc = lastBulletFired.GetComponent<BulletController>();

                if (bc != null)
                {
                    bc.firingWeapon = m_currentWeapon;
                    bc.trailSpawnPos = trailSpawn.position;
                }

                // AudioManager.audManInst.PlaySfx(gunShots, gunShot, transform.position);
                NetworkServer.Spawn(lastBulletFired);
                m_currentWeapon.currentAmmo--;
                canFire = false;

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

    [Command]
	public void CmdReloadWeapon()
	{
        Debug.Log("RELOAD");
		m_currentWeapon.currentAmmo = m_currentWeapon.maxAmmo;
	}

	IEnumerator StartAttackTimer(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		canFire = true;
	}

	IEnumerator StartReloadTimer(float seconds)
	{
        Debug.Log("Reloading...");
		yield return new WaitForSeconds(seconds);
		CmdReloadWeapon();
		canFire = true;
		isReloading = false;
	}


}