using UnityEngine;
using System.Collections;
using thelab.mvc;

public class PlayerController : Controller<KApplication>
{

	public override void OnNotification(string p_event, Object p_target, params object[] p_data)
	{
		switch (p_event)
		{
		case "take.damage" :
			app.model.player.currentHealth -= app.model.gun.power;
			break;
		}
	}
}
