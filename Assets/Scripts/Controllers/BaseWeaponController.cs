using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class BaseWeaponController : NetworkBehaviour
{
	public WeaponSettings[] equippableWeapons;
	public bool isReloading, canFire;
	public Transform playerCam;
	public string[] attackableTargets;
	public GameObject bulletPrefab;
	public Transform weaponSlotPos;

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

	public void FireWeapon()
	{
		if (!isReloading)
		{
			// shoot
			if (canFire)
			{
				Debug.Log ("FIRE");
				GameObject lastBulletFired = (GameObject)GameObject.Instantiate (CustomNetworkManager.Instance.spawnPrefabs[11], weaponSlotPos.position, weaponSlotPos.rotation);
				lastBulletFired.transform.forward = Camera.main.transform.forward;
				NetworkServer.Spawn (lastBulletFired);
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

	public void ReloadWeapon()
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
