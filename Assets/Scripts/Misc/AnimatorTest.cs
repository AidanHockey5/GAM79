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
        anim.SetFloat("Turn", h);
        anim.SetFloat("Run", v);
		bool fire1 = false;
		fire1 = Input.GetButton("Fire1");
		bool fire2 = false;
		fire2 = Input.GetButton("Fire2");
		bool fire3 = false;
		fire3 = Input.GetButton("Fire3");
		anim.SetBool("isAttacking", fire1);
		anim.SetBool("isGroundPound", fire2);
		anim.SetBool("isBreathAttack", fire3);
		if (fire1) {
			anim.SetTrigger ("Attack");
		}
		if (fire2) {
			anim.SetTrigger ("Ground Pound");
		}
		if (fire3) {			
			anim.SetTrigger ("Breath Attack");
		}
    }
}
