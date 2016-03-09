using UnityEngine;
using System.Collections;
using thelab.mvc;

public class KBaseModel : Model<KApplication>
{
	public MovementModel movement { get { return m_movement = Assert<MovementModel>(m_movement); } }
	private MovementModel m_movement;

	public CameraModel camera { get { return m_camera = Assert<CameraModel>(m_camera); } }
	private CameraModel m_camera;

	public InputModel input { get { return m_input = Assert<InputModel>(m_input); } }
	private InputModel m_input;

}
