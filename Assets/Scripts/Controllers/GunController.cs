using UnityEngine;
using System.Collections;
using thelab.mvc;

public class GunController : Controller <KApplication> 
{
	public override void OnNotification(string p_event, Object p_target, params object[] p_data)
	{
		switch (p_event)
		{
		case "fire.weapon.1":
			FireWeapon();
			break;
		case "reload.weapon":
			ReloadWeapon();
			break;
		}
	}

	public virtual void FireWeapon()
	{
		if (!app.model.gun.isReloading)
		{
			// shoot
			if (app.model.gun.canFire)
			{
				app.model.gun.targetHit = Physics.Raycast(app.view.camera.transform.position, app.view.camera.transform.forward, out app.model.gun.rayHit);
				app.model.gun.canFire = false;

				if (app.model.gun.targetHit)
				{
					for (int i = 0; i < app.model.gun.attackableTargets.Length; i++)
					{
						if (app.model.gun.rayHit.transform.tag == app.model.gun.attackableTargets[i])
						{
							Notify("take.damage", app.model.gun.power);
						}
					}
				}

				if (app.model.gun.currentAmmo <= 0)
				{
					app.model.gun.isReloading = true;
					StartCoroutine(StartReloadTimer(app.model.gun.reloadTime));
				}
				else
				{
					StartCoroutine(StartAttackTimer(app.model.gun.attackRate));
				}
			}
		}
	}

	public virtual void ReloadWeapon()
	{
		app.model.gun.currentAmmo = app.model.gun.maxAmmo;
	}

	IEnumerator StartAttackTimer(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		app.model.gun.currentAmmo--;
		app.model.gun.canFire = true;
	}

	IEnumerator StartReloadTimer(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		ReloadWeapon();
		app.model.gun.canFire = true;
		app.model.gun.isReloading = false;
	}
}
