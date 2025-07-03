using UnityEngine;

[CreateAssetMenu(fileName = "MeteorStrikePattern", menuName = "Scriptable Objects/MeteorStrikePattern")]
public class MeteorStrikePatternSO : BossPatternSO
{
    public GameObject warningEffectPrefab;
    public GameObject meteorEffectPrefab;
    public float warningDuration = 3f;
    public float damageRadius = 5f;
    public float damageAmount = 40f;
    public LayerMask targetMask;
    public override void Execute(GameObject boss)
    {
        boss.GetComponent<MeteorStrikePattern>()?.StartMeteorStrike(this);
        Debug.Log("메테오 진입");
    }
}
    
