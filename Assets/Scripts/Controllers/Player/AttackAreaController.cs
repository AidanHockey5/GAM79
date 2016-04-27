using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAreaController : MonoBehaviour 
{

    #region Public

    public List<GameObject> hitBuildings, hitPlayers = null;
    public MonsterAbilitySettings monsterAbilitySettings = null;

    #endregion

    #region Private

    float timeCounter;

    #endregion


    #region MonoBehaviours

    private void Start()
    {
        hitBuildings = new List<GameObject>();
        hitPlayers = new List<GameObject>();
        gameObject.SetActive(false);
        timeCounter = 0;
    }

    private void OnTriggerEnter(Collider col)
    {
        InstantDamage(col.gameObject);
    }

    private void OnTriggerStay(Collider col)
    {
        if (monsterAbilitySettings.isDamageOverTime)
        {
            DamageOverTime(col.gameObject);
        }
    }

    void OnEnable()
    {
        timeCounter = 0;
    }

    #endregion

    #region Damage

    private void InstantDamage(GameObject hitTarget)
    {
        Building hitBuilding = hitTarget.GetComponent<Building>();
        Rigidbody hitRigidbody = hitTarget.GetComponent<Rigidbody>();
        PlayerObject hitPlayer = hitTarget.GetComponent<PlayerObject>();

        if (hitBuilding != null)
        {
            hitBuilding.TakeDamage(monsterAbilitySettings.damage);

            if (hitRigidbody != null)
            {
                hitRigidbody.AddTorque(Vector3.forward * monsterAbilitySettings.physicsForce);
                hitRigidbody.AddForce(Vector3.forward * monsterAbilitySettings.physicsForce);
            }

            hitBuildings.Add(hitTarget);
        }

        if (hitPlayer != null)
        {
            hitPlayers.Add(hitTarget);
        }
    }

    private void DamageOverTime(GameObject hitTarget)
    {
        PlayerObject hitPlayer = hitTarget.GetComponent<PlayerObject>();

        if (hitPlayer != null)
        {
            timeCounter = timeCounter + Time.deltaTime;

            if (timeCounter > monsterAbilitySettings.tickRate)
            {
                hitPlayer.RequestTakeDamage(GameEvent.HIT_FROM_MONSTER, monsterAbilitySettings.damage);
                timeCounter = 0;
            }
        }
    }

    #endregion
}
