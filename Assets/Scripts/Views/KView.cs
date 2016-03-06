using UnityEngine;
using System.Collections;
using thelab.mvc;

public class KView : View<KApplication>
{
	public HumanView human { get { return m_human = Assert<HumanView>(m_human); } }
	private HumanView m_human;

	public CameraView camera { get { return m_camera = Assert<CameraView>(m_camera); } }
	private CameraView m_camera;
}
