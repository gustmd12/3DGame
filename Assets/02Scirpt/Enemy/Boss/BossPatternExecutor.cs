using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPatternExecutor : MonoBehaviour
{
    [SerializeField] BossPatternSO[] patternList;

    public void ExecuteRandomExecute()
    {
        int index = Random.Range(0, patternList.Length);
        StartCoroutine(ExecuteWithDelay(patternList[index]));
        
    }

    private IEnumerator ExecuteWithDelay(BossPatternSO pattern)
    {
        yield return new WaitForSeconds(pattern.delayBeforExecute);
        pattern.Execute(gameObject);
    }

}
