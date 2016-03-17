using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // exposed fields
    [SerializeField] float lookSensitivity = 5f;
    [SerializeField] float lookSmoothDamp = 0.1f;
    [SerializeField] int xLowerCameraBound = -75;
    [SerializeField] int xUpperCameraBound = 90;
    [SerializeField] Transform playerTransform = null;
    [SerializeField] float cameraOffset = 1;

    // camera movement
    private float yRotation, xRotation;
    private float currentYRotation, currentXRotation;
    private float yRotationVel, xRotationVel;
    private Camera playerCamera = null;

    void Start()
    {
        playerCamera = Camera.main;
    }

    public void Look(GameEvent eventType)
    {
        if (eventType == GameEvent.CAMERA_ROTATE)
        {
            Debug.Log("LOOK");
        }
    }

    private float DegreesToRadians(float degrees)
    {
        return degrees * (2 * Mathf.PI / 360);
    }
}
