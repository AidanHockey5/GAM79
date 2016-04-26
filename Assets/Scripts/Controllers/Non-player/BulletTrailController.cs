using UnityEngine;
using System.Collections;

public class BulletTrailController : MonoBehaviour
{

    #region Private

    private BulletSettings bullet;
	private new Transform transform;

    #endregion

    #region MonoBehaviours

    private void Start ()
	{
		transform = GetComponent<Transform> ();
		Destroy (gameObject, bullet.lifetime);
	}
	
	private void FixedUpdate ()
	{
		ApplyThrust ();
	}

    #endregion

    #region Functions

    private void ApplyThrust ()
	{
		transform.position += transform.forward * bullet.speed * Time.fixedDeltaTime;
	}

    #endregion

}
