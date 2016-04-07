using UnityEngine;

[System.Serializable]
public class CameraSettings
{
    public Vector3 targetPositionOffset = new Vector3(0, 3.4f, 0);
    public float lookSmooth = 100f;
    public float distanceFromTarget = -8;
    public float zoomSmooth = 10;
    public float maxZoom = -2;
    public float minZoom = -15;
    public float xRotation = -20;
    public float yRotation = -180;
    public float maxXRotation = 25;
    public float minXRotation = -85;
    public float vOrbitSmooth = 150;
    public float hOrbitSmooth = 150;

    public CameraSettings()
	{
		
	}
}
