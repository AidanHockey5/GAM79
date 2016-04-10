using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ParticleTestScript : NetworkBehaviour
{
    ParticleManager pm;

	// Use this for initialization
	void Start ()
    {
        pm = ParticleManager.Instance;       
	}

    void Update()
    {
        if (!isLocalPlayer)
        {
            //return;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            int r = Random.Range(0, pm.particleSystems.Count);
            pm.CmdSpawnParticleWithRotation(pm.particleSystems[r].name, pm.transform.position, this.transform.eulerAngles, 10);
        }
    }

}
