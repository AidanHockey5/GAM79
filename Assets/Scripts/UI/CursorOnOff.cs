using UnityEngine;
using System.Collections;

public class CursorOnOff
{

	public static void ChangeCursorState(bool onOff)
	{
		if (onOff)
		{
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

}
