using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using System.Collections;


public class BaseWeaponController : NetworkBehaviour
{
    public AudioClip gunShot = null;
    public AudioMixerGroup gunShots = null;
    
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
				Quaternion bulletRotation = Quaternion.LookRotation (Camera.main.transform.forward, transform.up);
				GameObject lastBulletFired = (GameObject)GameObject.Instantiate (CustomNetworkManager.Instance.spawnPrefabs[11], weaponSlotPos.position, weaponSlotPos.rotation);
                AudioManager.audManInst.PlaySfx(gunShots, gunShot, transform.position);
				NetworkServer.Spawn (lastBulletFired);
				//targetHit = Physics.Raycast(playerCam.position, playerCam.forward, out rayHit);
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
								targetHealth.TakeDamage(10);
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
