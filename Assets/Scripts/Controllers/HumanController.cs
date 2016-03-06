using UnityEngine;
using System.Collections;
using thelab.mvc;

public class HumanController : KController 
{

	public override void OnNotification(string p_event, Object p_target, params object[] p_data)
	{
		switch (p_event)
		{
			// move player view
		}
	}
}
