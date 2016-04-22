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
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
    public Transform trailSpawn;

	private WeaponSettings currentWeapon;

	RaycastHit rayHit;
	bool targetHit;

	// Use this for initialization
	void Start () 
	{
		SetWeapon (0);
	}

	void SetWeapon(int slot)
	{
		currentWeapon = equippableWeapons[slot];
	}

	void GetInput()
	{
		if (!UIManager.Instance.IsPlayable())
			return;
		
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
                    bc.firingWeapon = currentWeapon;
                    bc.trailSpawnPos = trailSpawn.position;
                }

                NetworkServer.Spawn(lastBulletFired);
                currentWeapon.currentAmmo--;
                canFire = false;

				if (currentWeapon.currentAmmo <= 0)
				{
					isReloading = true;
					StartCoroutine(StartReloadTimer(currentWeapon.reloadTime));
				}
				else
				{
					StartCoroutine(StartAttackTimer(currentWeapon.attackRate));
				}
			}
		}
	}

    [Command]
	public void CmdReloadWeapon()
	{
        Debug.Log("RELOAD");
		currentWeapon.currentAmmo = currentWeapon.maxAmmo;
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
