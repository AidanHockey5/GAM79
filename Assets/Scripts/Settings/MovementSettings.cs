using UnityEngine;

[System.Serializable]
public class MovementSettings
{
    public float accelerationRate = 10;
    public float maxSpeed = 20;
    public float turnRate = 100;
    public float gravity = -9.81f;
    public float inputDelay = 0.1f;
}