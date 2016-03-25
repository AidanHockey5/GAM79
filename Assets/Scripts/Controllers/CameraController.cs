using UnityEngine;
using System;
using System.Collections;

public class CameraController : MonoBehaviour, IEventListener
{
	// exposed fields
	[SerializeField] private Transform m_target;
	[SerializeField] private PositionSettings position;
	[SerializeField] private OrbitSettings orbit = new OrbitSettings();
	[SerializeField] private InputSettings input = new InputSettings();


	// private members
	private Vector3 m_targetPos = Vector3.zero;
	private Vector3 destination = Vector3.zero;
	private Quaternion m_targetRotation = Quaternion.identity;
    private MovementController m_movementController;
	private float m_vOrbitInput, m_hOrbitInput, m_zoomInput, m_hOrbitSnapInput;

	// static members
	private static CameraController m_instance = null;

	// singleton
	public static CameraController Instance
	{
		get
		{
			if (m_instance != null)
			{
				return m_instance;
			}
			else
			{
				GameObject go = new GameObject();
				return m_instance = go.AddComponent<CameraController>();
			}
		}
	}


	void Awake()
	{
		m_instance = this;
	}

	void Start()
	{
		Subscribe(this, SnapToBehindTarget);
		Subscribe(this, OrbitTarget); 
		Subscribe(this, ZoomInOnTarget); 
	}

	void OnDestroy()
	{
		UnSubscribe(this, SnapToBehindTarget);
		UnSubscribe(this, OrbitTarget); 
		UnSubscribe(this, ZoomInOnTarget); 
	}

	public void SetCameraTarget(Transform t, PositionSettings cameraPos)
    {
        m_target = t;
		position = cameraPos;

        if (m_target != null)
        {
            if (m_target.GetComponent<MovementController>())
            {
                m_movementController = m_target.GetComponent<MovementController>();
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

    void LateUpdate()
    {
		if (m_target != null)
		{
			// moving
			MoveToTarget();
			// rotating
			LookAtTarget();
		}
    }

    void MoveToTarget()
    {
        m_targetPos = m_target.position + position.targetPositionOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + m_target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += m_targetPos;
        transform.position = destination;
    }

    void LookAtTarget()
    {
		m_targetRotation = Quaternion.LookRotation(m_targetPos - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotation, position.lookSmooth * Time.deltaTime);
    }

	void SnapToBehindTarget(object sender, EventArgs e)
	{
		// args[o] - float hOrbitSnapInput
		GameEventArgs args = (GameEventArgs)e;

		if (args.eventType == GameEvent.CAMERA_SNAP)
		{
			Debug.Log("SNAP");

			m_hOrbitSnapInput = (float)args.eventArgs[0];

			if (m_hOrbitSnapInput > 0)
			{
				orbit.yRotation = -180;
			}
		}
	}

	void OrbitTarget(object sender, EventArgs e)
    {
		// args[o] - float hOrbitInput
		// args[1] - float vOrbitInput
		GameEventArgs args = (GameEventArgs)e;

		if (args.eventType == GameEvent.CAMERA_ORBIT)
		{
			Debug.Log("ORBIT");

			m_hOrbitInput = (float)args.eventArgs[0];
			m_vOrbitInput = (float)args.eventArgs[1];

			orbit.xRotation += -m_vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
			orbit.yRotation += -m_hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

			if (orbit.xRotation > orbit.maxXRotation)
			{
				orbit.xRotation = orbit.maxXRotation;
			}

			if (orbit.xRotation < orbit.minXRotation)
			{
				orbit.xRotation = orbit.minXRotation;
			}
		}
    }

	void ZoomInOnTarget(object sender, EventArgs e)
    {
		// args[o] - float zoomInput
		GameEventArgs args = (GameEventArgs)e;

		if (args.eventType == GameEvent.CAMERA_ZOOM)
		{
			Debug.Log("ZOOM");
			m_zoomInput = (float)args.eventArgs[0];

			position.distanceFromTarget += m_zoomInput * position.zoomSmooth * Time.deltaTime;

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

	// IEventListener implementation
	public void Subscribe(object subscriber, EventHandler<GameEventArgs> p_handler)
	{
		InputManager.Instance.RegisterHandler(p_handler);
	}

	public void UnSubscribe(object subscriber, EventHandler<GameEventArgs> p_handler)
	{
		InputManager.Instance.UnRegisterHandler(p_handler);	
	}



}
