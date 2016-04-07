using UnityEngine;
using System;
using System.Collections;

public class CameraController : MonoBehaviour, IEventListener
{

    #region Public Fields

    #endregion

    #region Private Members

    private Camera mainCam = null;
    private Transform viewPoint = null;
    private Transform viewTarget = null;
    private CameraSettings camSettings = null;
    private OrbitSettings orbit = null;
    private Vector3 tarPos = Vector3.zero;
    private Vector3 destination = Vector3.zero;
    private Quaternion tarRotation = Quaternion.identity;
    private float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput = 0.0f;

    #endregion

    #region MonoBehaviours

    void Start()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        Subscribe();
    }

    void OnDestroy()
    {
        UnSubscribe();
    }

    void LateUpdate()
    {
        if (viewTarget != null)
        {
            MoveToTarget();
            LookAtTarget();
        }
    }

    #endregion

    #region Public Functions

    public void SetCameraTarget(Transform t, CameraSettings camSettings)
    {
        viewTarget = t;
        this.camSettings = camSettings;

        if (viewTarget == null)
        {
            Debug.LogError("Your camera needs a target.");
        }
    }

    #endregion

    #region Private Functions

    void MoveToTarget()
    {
        tarPos = viewTarget.position + camSettings.targetPositionOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + viewTarget.eulerAngles.y, 0) * -Vector3.forward * camSettings.distanceFromTarget;
        destination += tarPos;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        /*
        yRotation += mouseXInput * lookSensitivity;
		xRotation += mouseYInput * lookSensitivity;
		xRotation = Mathf.Clamp(xRotation, xLowerCameraBound, xUpperCameraBound);
		currentXRotation = Mathf.SmoothDamp(currentXRotation, xRotation, ref xRotationVel, lookSmoothDamp);
		currentYRotation = Mathf.SmoothDamp(currentYRotation, yRotation, ref yRotationVel, lookSmoothDamp);
		transform.rotation = Quaternion.Euler(0, currentYRotation, 0);
        targetRotation = transform.rotation;
        playerCam.transform.localRotation = Quaternion.Euler(-currentXRotation, 0, 0);
        */
        tarRotation = Quaternion.LookRotation(viewTarget.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, tarRotation, camSettings.lookSmooth * Time.deltaTime);
    }

    #endregion

    #region EventHandler

    void SnapToBehindTarget(float hOrbitSnapInput)
	{
		if (hOrbitSnapInput > 0)
		{
			orbit.yRotation = -180;
		}
	}

	void OrbitTarget(float hOrbitInput, float vOrbitInput)
    {
		orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
		orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

		if (orbit.xRotation > orbit.maxXRotation)
		{
			orbit.xRotation = orbit.maxXRotation;
		}

		if (orbit.xRotation < orbit.minXRotation)
		{
			orbit.xRotation = orbit.minXRotation;
		}
    }

	void ZoomInOnTarget(float zoomInput)
    {
		camSettings.distanceFromTarget += zoomInput * camSettings.zoomSmooth * Time.deltaTime;

		if (camSettings.distanceFromTarget > camSettings.maxZoom)
		{
			camSettings.distanceFromTarget = camSettings.maxZoom;
		}

		if (camSettings.distanceFromTarget < camSettings.minZoom)
		{
			camSettings.distanceFromTarget = camSettings.minZoom;
		}
    }

    #endregion

    #region IEventListener

    public void Subscribe()
    {
        PlayerObject playerObj = null;
        playerObj = GetComponent<PlayerObject>();

        if (playerObj != null)
        {
            playerObj.RegisterHandler(ReceiveBroadcast);
        }
    }

    public void UnSubscribe()
    {
        PlayerObject playerObj = null;
        playerObj = GetComponent<PlayerObject>();

        if (playerObj != null)
        {
            playerObj.UnRegisterHandler(ReceiveBroadcast);
        }
    }

    public void ReceiveBroadcast(object sender, GameEventArgs gameEventArgs)
    {
        switch (gameEventArgs.eventType)
        {
            case GameEvent.CAMERA_ORBIT:
                {
                    // index 0 = hOrbitInput, index 1 = vOrbitInput
                    OrbitTarget((float)gameEventArgs.eventArgs[0], (float)gameEventArgs.eventArgs[1]);
                }
                break;
            case GameEvent.CAMERA_SNAP:
                {
                    // index 0 = hOrbitSnapInput
                    SnapToBehindTarget((float)gameEventArgs.eventArgs[0]);
                }
                break;
            case GameEvent.CAMERA_ZOOM:
                {
                    // index 0 = zoomInput
                    ZoomInOnTarget((float)gameEventArgs.eventArgs[0]);
                }
                break;
        }
}
    #endregion


}
