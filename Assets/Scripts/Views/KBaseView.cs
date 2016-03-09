using UnityEngine;
using System.Collections;
using thelab.mvc;

public class KBaseView : View<KApplication>
{
	public MovementView movement { get { return m_movement = Assert<MovementView>(m_movement); } }
	private MovementView m_movement;

	public CameraView camera { get { return m_camera = Assert<CameraView>(m_camera); } }
	private CameraView m_camera;

	public InputView input { get { return m_input = Assert<InputView>(m_input); } }
	private InputView m_input;
}
