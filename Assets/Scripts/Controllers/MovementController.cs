using UnityEngine;
using System.Collections;
using thelab.mvc;

public class MovementController : Controller<KApplication> 
{
	private Vector3 targetVel, velocityChange;

	public override void OnNotification(string p_event, Object p_target, params object[] p_data)
	{
		switch (p_event)
		{
		case "run" :
			Run();
			break;
		}
	}

	void Run()
	{
		if (Mathf.Abs(app.model.input.verticalAxisInput) > app.model.input.inputDelay || Mathf.Abs(app.model.input.horizontalAxisInput) > app.model.input.inputDelay)
		{
			// find new direction vector
			targetVel = new Vector3(app.model.input.horizontalAxisInput, 0, app.model.input.verticalAxisInput);
			targetVel = transform.TransformDirection(targetVel);
			targetVel.Normalize();
			// increase speed
			targetVel *= app.model.movement.acceleration;
			velocityChange = targetVel - app.view.movement.rigidBody.velocity;
			// clamp velocitychange to limit speed
			velocityChange.x = Mathf.Clamp(velocityChange.x, -app.model.movement.maxVelocityChange, app.model.movement.maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -app.model.movement.maxVelocityChange, app.model.movement.maxVelocityChange);
			velocityChange.y = 0;
			app.view.movement.rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
		}
		else
		{
			// zero velocity
			app.view.movement.rigidBody.velocity = Vector3.zero;
		}
	}
}
