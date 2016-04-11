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

    [ServerCallback]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int r = Random.Range(0, pm.particleSystems.Count);
            pm.RpcSpawnParticleWithRotation(pm.particleSystems[r].name, pm.transform.position, this.transform.eulerAngles, 10);
        }
    }

}
