using UnityEngine;

[System.Serializable]
public class WeaponSettings
{
    public string name = "Gun";
	public float attackRate = 1.0f;
	public float reloadTime = 3.0f;
	public float range = 1000.0f;
	public int power = 1;
	public int maxAmmo = 1;
	public int currentAmmo = 0;
}
