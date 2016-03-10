using UnityEngine;
using System.Collections;
using thelab.mvc;

public class InputView : View<KApplication>
{

	void GetInput()
	{
		// keyboard movement
		app.model.input.verticalAxisInput = Input.GetAxis("Vertical");
		app.model.input.horizontalAxisInput = Input.GetAxis("Horizontal");
		Notify("player.movement");
		// mouse movement
		app.model.input.mouseXInput = Input.GetAxis("Mouse X");
		app.model.input.mouseYInput = Input.GetAxis("Mouse Y");
		Notify("player.look");

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			CursorOnOff.ChangeCursorState(true);
		}

		// left mouse
		if (Input.GetMouseButton(0))
		{
			CursorOnOff.ChangeCursorState(false);
			Notify("player.fire.mouse.0");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			Notify("player.keydown.r");
		}
	}

	void Update()
	{
		GetInput();
	}
}

