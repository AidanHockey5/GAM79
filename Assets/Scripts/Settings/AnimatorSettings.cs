using UnityEngine;

[System.Serializable]
public class AnimatorSettings
{
    public MonsterState currentState = MonsterState.IDLE;
    public string RUN_FLOAT = "Run";
    public string TURN_STRING = "Turn";
    public string ISMOVING_BOOL = "isMoving";
    public string ISATTACKING_BOOL = "isAttacking";
    public string SPEED_CURVE = "Speed";
}
