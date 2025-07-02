using UnityEngine;

[CreateAssetMenu(fileName = "FlameBreathPatternSO", menuName = "Scriptable Objects/FlameBreathPatternSO")]
public class FlameBreathPatternSO : BossPatternSO
{
    public float range, angle, damagePerSecond, duration;
    public LayerMask targetMask;
    public GameObject flameEffectPrefab;

    public override void Execute(GameObject boss)
    {
        boss.GetComponent<BossFlameBreath>()?.StartFlameBreath(this);
    }
}
