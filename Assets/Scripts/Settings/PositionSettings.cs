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
}
