using UnityEngine;
using System.Collections;

public class MonsterAbilityManager : MonoBehaviour {

	[SerializeField] GameObject[] attackAreaGameObjects;
	[HideInInspector] public TelegraphedAttack[] attackAreaScripts;

    void Start ()
    {
		attackAreaScripts = new TelegraphedAttack[attackAreaGameObjects.Length];

		for(int i = 0; i < attackAreaGameObjects.Length; i++)
		{
			attackAreaScripts[i] = attackAreaGameObjects[i].GetComponent<TelegraphedAttack>();
		}
	}

    public void AttackArea_ON(int attackAreaIndex)
    {
        attackAreaGameObjects[attackAreaIndex].SetActive(true);
    }

    public void AttackArea_OFF(int attackAreaIndex)
    {
        attackAreaGameObjects[attackAreaIndex].SetActive(false);
    }

    public GameObject GetAttackArea(int index)
    {
        return attackAreaGameObjects[index];
    }
}
