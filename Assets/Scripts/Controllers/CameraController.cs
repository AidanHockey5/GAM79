using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();

    private Vector3 targetPosition = Vector3.zero;
    private Vector3 destination = Vector3.zero;
    private MovementController movementController;
    private float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;

    void Start()
    {
        SetCameraTarget(target);
        MoveToTarget();
    }

    void SetCameraTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            if (target.GetComponent<MovementController>())
            {
                movementController = target.GetComponent<MovementController>();
            }
            else
            {
                Debug.LogError("The camera's target needs a movement controller.");
            }
        }
        else
        {
            Debug.LogError("Your camera needs a target.");
        }
    }

    //public void Look(GameEvent eventType)
    //{
    //    if (eventType == GameEvent.CAMERA_ROTATE)
    //    {
    //        Look();
    //    }
    //}

    void GetInput()
    {
        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    }

    void Update()
    {
        GetInput();
        OrbitTarget();
        ZoomInOnTarget();
    }

    void LateUpdate()
    {
        // moving
        MoveToTarget();
        // rotating
        LookAtTarget();
    }

    void MoveToTarget()
    {
        targetPosition = target.position + position.targetPositionOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPosition;
        transform.position = destination;
    }

    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
    }

    void OrbitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            // place camera behind target
            orbit.yRotation = -180;
        }

        // orbit based on axis input
        orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

        // min and max vertical 
        if (orbit.xRotation > orbit.maxXRotation)
        {
            orbit.xRotation = orbit.maxXRotation;
        }

        if (orbit.xRotation < orbit.minXRotation)
        {
            orbit.xRotation = orbit.minXRotation;
        }
    }

    void ZoomInOnTarget()
    {
        position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;

        if (position.distanceFromTarget > position.maxZoom)
        {
            position.distanceFromTarget = position.maxZoom;
        }

        if (position.distanceFromTarget < position.minZoom)
        {
            position.distanceFromTarget = position.minZoom;
        }


    }

}
