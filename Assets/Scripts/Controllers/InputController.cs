using UnityEngine;
using System.Collections;
using thelab.mvc;

public class InputController : Controller<KApplication>
{
	public override void OnNotification(string p_event, Object p_target, params object[] p_data)
	{
		switch(p_event)
		{
		case "player.movement" :
			Notify("run");
			break;
		case "player.look" :
			Notify("look");
			break;
		case "player.fire.mouse.0" :
			Notify("fire.weapon.1");
			break;
		}
	}
}

