using UnityEngine;
using System.Collections;

public class CarCamera : MonoBehaviour
{

    public Transform lookTarget;
    public Transform moveTarget;
    public float speed = 6;
    public float lerpDist = 3;
    public Transform visual;
    
	void FixedUpdate ()
    {
        float d = Time.fixedDeltaTime;
        transform.LookAt(lookTarget.position);


        RaycastHit hit;
        Vector3 targetPosition = moveTarget.position;

        if (Physics.Linecast(moveTarget.position, transform.position, out hit))
        {
            targetPosition = hit.point + (hit.normal * -1 * 0.5f);
            visual.position = hit.point;
        }


        if (Vector3.Distance(transform.position, targetPosition) > lerpDist)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, d * speed);
        }
        else
        {
            Vector3 dir = (targetPosition - transform.position).normalized;
            Vector3 newPos = transform.position + dir * speed * d;
            if (Vector3.Distance(transform.position, targetPosition) > Vector3.Distance(transform.position, newPos))
                transform.position = newPos;
            else
                transform.position = targetPosition;
        }

    }
}
