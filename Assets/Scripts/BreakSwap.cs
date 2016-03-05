using UnityEngine;
using System.Collections;

public class BreakSwap : MonoBehaviour
{
    [SerializeField]
    GameObject breakForm;
    
    // Use this for initialization
    void Start ()
    {
        if (breakForm == null)
        {
            breakForm = null;
        }
        
	}

    public void BreakingTime()
    {
        RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector3.up);
        if (hit.Length > 0)
        {
            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.gameObject != this.gameObject)
                {
                    if (hit[i].transform.gameObject.GetComponent<BreakSwap>() != null)
                    {
                        DestroyLevel(hit[i].transform.gameObject);
                        StartCoroutine(Wait());
                    }
                }
            }
        }
        DestroyLevel(this.gameObject);
    }

    void DestroyLevel(GameObject level)
    {
        Instantiate(level.GetComponent<BreakSwap>().breakForm, transform.position, transform.rotation);
        Destroy(level);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
    }
}