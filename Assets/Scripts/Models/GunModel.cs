using UnityEngine;
using System.Collections;
using thelab.mvc;

public class GunModel : Model<KApplication> 
{
	public float attackRate, reloadTime, range;
	public int power, maxAmmo, currentAmmo;
	public bool isReloading, canFire;
	public Transform playerCam;
	public string[] attackableTargets;

	public RaycastHit rayHit;
	public bool targetHit;

}
