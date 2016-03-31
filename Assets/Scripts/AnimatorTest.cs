using UnityEngine;
using System.Collections;

public class AnimatorTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Animator anim = GetComponent<Animator>();
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        anim.SetFloat("HORIZONTAL", h);
        anim.SetFloat("VERTICAL", v);
        anim.SetBool("isAttacking", Input.GetButton("Fire1"));
    }
}
