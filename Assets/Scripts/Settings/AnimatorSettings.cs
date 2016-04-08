using UnityEngine;

[System.Serializable]
public class AnimatorSettings
{
    public MonsterState currentState = MonsterState.IDLE;
    public string RUN_FLOAT = "Run";
    public string TURN_FLOAT = "Turn";
    public string ISMOVING_BOOL = "isMoving";
    public string FIRE_1_BOOL = "isAttacking";
	public string FIRE_2_BOOL = "isGroundPound";
	public string FIRE_3_BOOL = "isBreathAttack";
	public string FIRE_1_TRIGGER = "Attack";
	public string FIRE_2_TRIGGER = "Ground Pound";
	public string FIRE_3_TRIGGER = "Breath Attack";
}
