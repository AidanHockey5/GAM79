using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class ParticleManager : NetworkBehaviour
{
    public List<GameObject> particleSystems;

    private Dictionary<string, GameObject> pickAParticle;

    private static ParticleManager instance_ = null;

    public static ParticleManager Instance
    {
        get
        {
            if (instance_ != null)
            {
                return instance_;
            }
            else
            {
                GameObject go = new GameObject();
                return instance_ = go.AddComponent<ParticleManager>();
            }
        }
    }
    void Awake()
    {
        instance_ = this;
    }

    void Start ()
    {
        pickAParticle = new Dictionary<string, GameObject>();
        if (particleSystems != null && particleSystems.Count > 0)
        {
            foreach (GameObject p in particleSystems)
            {
                pickAParticle.Add(p.name, p);
            }
        }
        else
        {
            print("No particles Set");
        }
	}

    [ClientRpc]
    public void RpcSpawnParticleWithRotation(string particleName, Vector3 pos, Vector3 rot, float lifeTime)
    {
        print("Command reached");
        if (particleSystems != null && particleSystems.Count > 0)
        {
            GameObject particle = Instantiate(pickAParticle[particleName], pos, Quaternion.Euler(rot)) as GameObject;

            if (lifeTime > 0)
            {
                Destroy(particle, lifeTime);
            }
        }
        else
        {
            print("No particles to Spawn");
        }
    }

    [ClientRpc]
    public void RpcSpawnParticle(string particleName, Vector3 pos, float lifeTime)
    {
        print("Command reached");
        if (particleSystems != null && particleSystems.Count > 0)
        {
            GameObject particle = Instantiate(pickAParticle[particleName], pos, Quaternion.Euler(Vector3.zero)) as GameObject;

            if (lifeTime > 0)
            {
                Destroy(particle, lifeTime);
            }
        }
        else
        {
            print("No particles to Spawn");
        }
    }

}
