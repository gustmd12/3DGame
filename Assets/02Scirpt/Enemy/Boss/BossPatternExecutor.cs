using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPatternExecutor : MonoBehaviour
{
    BossPatternSO bossPatternSO;

    [SerializeField] BossPatternSO[] patternList;

    
    public void Execute(BossPatternSO pattern)
    {
        switch (pattern.Type)
        {
            case PatternType.FlameBreath:
                GetComponent<BossFlameBreath>()?.StartFlameBreath((FlameBreathPatternSO)pattern);
                break;
            case PatternType.MeteorStrike:
                GetComponent<MeteorStrikePattern>()?.StartMeteorStrike((MeteorStrikePatternSO)pattern);
                break;
        }

    }

    public void StopAllPattern()
    {
        GetComponent<BossFlameBreath>()?.StopBreath();
        GetComponent<MeteorStrikePattern>()?.StopMeteor();
    }

}
