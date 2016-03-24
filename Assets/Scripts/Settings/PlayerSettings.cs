using UnityEngine;

[System.Serializable]
public class PlayerSettings
{
    public PlayerType playerType;
    public int maxHealth;
    public float accelerationRate = 10;
    public float maxVelocityChange = 10;
    public float turnRate = 100;
    public float inputDelay = 0.1f; 
}
