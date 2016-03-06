using UnityEngine;
using System.Collections;
using thelab.mvc;

public class KModel : Model<KApplication>
{
	public HumanModel human { get { return m_human = Assert<HumanModel>(m_human); } }
	private HumanModel m_human;

}
