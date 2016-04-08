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
	private CameraSettings cameraSettings = null;
    private Vector3 tarPos = Vector3.zero;
    private Vector3 destination = Vector3.zero;
    private Quaternion tarRotation = Quaternion.identity;
    private float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput = 0.0f;

    #endregion

    #region MonoBehaviours

    void Start()
    {
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
		if (mainCam == null)
		{
			GameObject cameraObj = new GameObject("Main Camera");
			mainCam = cameraObj.AddComponent<Camera>();
			viewPoint = mainCam.transform;
			viewTarget = t;
		}
		else
		{
			viewPoint = mainCam.transform;
			viewTarget = t;
		}
    }

    #endregion

    #region Private Functions

    void MoveToTarget()
    {
		if (viewTarget != null && viewPoint != null)
		{
			tarPos = viewTarget.position + cameraSettings.targetPositionOffset;
			destination = Quaternion.Euler(cameraSettings.xRotation, cameraSettings.yRotation + viewTarget.eulerAngles.y, 0) * -Vector3.forward * cameraSettings.distanceFromTarget;
			destination += tarPos;
			viewPoint.position = destination;
		}
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

		if (viewTarget != null && viewPoint != null)
		{
			tarRotation = Quaternion.LookRotation(viewTarget.position - viewPoint.position);
			mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, tarRotation, cameraSettings.lookSmooth * Time.deltaTime);
		}
    }

    #endregion

    #region EventHandler

    void SnapToBehindTarget(float hOrbitSnapInput)
	{
		if (hOrbitSnapInput > 0)
		{
			cameraSettings.yRotation = -180;
		}
	}

	void OrbitTarget(float hOrbitInput, float vOrbitInput)
    {
		cameraSettings.xRotation += -vOrbitInput * cameraSettings.vOrbitSmooth * Time.deltaTime;
		cameraSettings.yRotation += -hOrbitInput * cameraSettings.hOrbitSmooth * Time.deltaTime;

		if (cameraSettings.xRotation > cameraSettings.maxXRotation)
		{
			cameraSettings.xRotation = cameraSettings.maxXRotation;
		}

		if (cameraSettings.xRotation < cameraSettings.minXRotation)
		{
			cameraSettings.xRotation = cameraSettings.minXRotation;
		}
    }

	void ZoomInOnTarget(float zoomInput)
    {
		cameraSettings.distanceFromTarget += zoomInput * cameraSettings.zoomSmooth * Time.deltaTime;

		if (cameraSettings.distanceFromTarget > cameraSettings.maxZoom)
		{
			cameraSettings.distanceFromTarget = cameraSettings.maxZoom;
		}

		if (cameraSettings.distanceFromTarget < cameraSettings.minZoom)
		{
			cameraSettings.distanceFromTarget = cameraSettings.minZoom;
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
			cameraSettings = playerObj.cameraSettings;
        }
    }

    public void UnSubscribe()
    {
        PlayerObject playerObj = null;
        playerObj = GetComponent<PlayerObject>();

        if (playerObj != null)
        {
            playerObj.UnRegisterHandler(ReceiveBroadcast);
			cameraSettings = null;
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
