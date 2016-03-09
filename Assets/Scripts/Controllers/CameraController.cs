using UnityEngine;
using System.Collections;
using thelab.mvc;

public class CameraController : Controller <KApplication>
{
	public override void OnNotification(string p_event, Object p_target, params object[] p_data)
	{
		switch (p_event)
		{
		case "look" :
			Look();
			break;
		}
	}

	void Look()
	{
		app.model.camera.yRotation += app.model.input.mouseXInput * app.model.camera.lookSensitivity;
		app.model.camera.xRotation += app.model.input.mouseYInput * app.model.camera.lookSensitivity;
		app.model.camera.xRotation = Mathf.Clamp(app.model.camera.xRotation, app.model.camera.xLowerCamBound, app.model.camera.xUpperCamBound);
		app.model.camera.currentXRotation = Mathf.SmoothDamp(app.model.camera.currentXRotation, app.model.camera.xRotation, ref app.model.camera.xRotationVel, app.model.camera.lookSmoothDamp);
		app.model.camera.currentYRotation = Mathf.SmoothDamp(app.model.camera.currentYRotation, app.model.camera.yRotation, ref app.model.camera.yRotationVel, app.model.camera.lookSmoothDamp);
		app.view.movement.transform.rotation = Quaternion.Euler(0, app.model.camera.currentYRotation, 0);
		app.view.camera.transform.localRotation = Quaternion.Euler(-app.model.camera.currentXRotation, 0, 0);
	}
}

