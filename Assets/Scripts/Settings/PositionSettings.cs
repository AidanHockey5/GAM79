using UnityEngine;

[System.Serializable]
public class PositionSettings
{
    public Vector3 targetPositionOffset = new Vector3(0, 3.4f, 0);
    public float lookSmooth = 100f;
    public float distanceFromTarget = -8;
    public float zoomSmooth = 10;
    public float maxZoom = -2;
    public float minZoom = -15;

	public PositionSettings()
	{
		
	}

	public PositionSettings(Vector3 tarPosOffset, float lookSmooth, float distFromTar, float zoomSmooth, float maxZoom, float minZoom)
	{
		this.targetPositionOffset = tarPosOffset;
		this.lookSmooth = lookSmooth;
		this.distanceFromTarget = distFromTar;
		this.zoomSmooth = zoomSmooth;
		this.maxZoom = maxZoom;
		this.minZoom = minZoom;
	}
}
