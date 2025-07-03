using UnityEngine;

public enum PatternType { FlameBreath, MeteorStrike}

[CreateAssetMenu(fileName = "BossPatarnSO", menuName = "Scriptable Objects/BossPatarnSO")]
public abstract class BossPatternSO : ScriptableObject
{
    public string pattternName = "LaserPattern";
    public float delayBeforExecute = 1f;

    
    public PatternType Type;
    public float coolDown = 5f;
    public float triggerHPPercent = 0f;

    public abstract void Execute(GameObject boss);

}
