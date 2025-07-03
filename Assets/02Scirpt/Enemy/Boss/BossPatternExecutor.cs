using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPatternExecutor : MonoBehaviour
{
    BossPatternSO bossPatternSO;

    [SerializeField] BossPatternSO[] patternList;

    //public void ExecuteRandomExecute()
    //{
    //    int index = Random.Range(0, patternList.Length);
    //    StartCoroutine(ExecuteWithDelay(patternList[index]));
        
    //}

    //private IEnumerator ExecuteWithDelay(BossPatternSO pattern)
    //{
    //    yield return new WaitForSeconds(pattern.delayBeforExecute);
    //    pattern.Execute(gameObject);
    //}

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

}
