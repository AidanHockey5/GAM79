using UnityEngine;
using System.Collections;
using thelab.mvc;

public class HumanView : KView
{

	void GetInput()
	{
		app.model.human.verticalAxisInput = Input.GetAxis("Vertical");
		app.model.human.horizontalAxisInput = Input.GetAxis("Horizontal");
		app.model.human.mouseXInput = Input.GetAxis("Mouse X");
		app.model.human.mouseYInput = Input.GetAxis("Mouse Y");

		Notify("player.movement");

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CursorOnOff.ChangeCursorState(true);
		}

		if (Input.GetMouseButton(0))
		{
			CursorOnOff.ChangeCursorState(false);
		}
	}

	void Update()
	{
		GetInput();
	}
}
