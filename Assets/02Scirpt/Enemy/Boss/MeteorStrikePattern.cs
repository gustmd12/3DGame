using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStrikePattern : MonoBehaviour
{
    private Coroutine meteorRoutine;
    private BossAnimationController bossAnimationController;

    private void Awake()
    {
        bossAnimationController = GetComponent<BossAnimationController>();
    }

    public void StartMeteorStrike(MeteorStrikePatternSO pattern)
    {
        if (meteorRoutine != null)
            StopCoroutine(meteorRoutine);

        meteorRoutine = StartCoroutine(MeteorRoutine(pattern));
    }


    private IEnumerator MeteorRoutine(MeteorStrikePatternSO pattern)
    {
        bossAnimationController.MeteorAnim();

        Transform player = GameObject.FindWithTag("Player")?.transform;
        if(player == null) yield break;

        Vector3 targetPosition = player.position;

        GameObject warning = null;
        Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
        if (pattern.warningEffectPrefab != null)
        {
            warning = Instantiate(pattern.warningEffectPrefab, targetPosition, rotation);
        }

        yield return new WaitForSeconds(pattern.warningDuration);

        if(warning != null)
            Destroy(warning);

        GameObject meteor = null;
        if(pattern.meteorEffectPrefab != null)
        {
            Vector3 spawnPos = targetPosition + Vector3.up * 20f;
            Vector3 spawnPos2 = targetPosition;

            meteor = Instantiate(pattern.meteorEffectPrefab, spawnPos2, Quaternion.identity);
            
        }


        Collider[] hits = Physics.OverlapSphere(targetPosition, pattern.damageRadius, pattern.targetMask);
        foreach (var hit in hits)
        {
            IDamaged damaged = hit.GetComponent<IDamaged>();
            if (damaged != null)
            {
                damaged.TakeDamage(pattern.damageAmount);
                Debug.Log($"운석 히트 {hit.name}");
            }
        }
        
        if (meteor != null)
            Destroy(meteor, 3f);
    }

    public void StopMeteor()
    {
        if(meteorRoutine != null)
        {
            StopCoroutine(meteorRoutine);
            meteorRoutine = null;
        }
        
    }

}
