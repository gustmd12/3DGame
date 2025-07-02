using System.Collections;
using TreeEditor;
using UnityEngine;

public class BossFlameBreath : MonoBehaviour
{
    [SerializeField] Transform spawnPos;
    //private Vector3 offset = new Vector3(0, 2, 4);

    private Coroutine flameRoutine;

    private BossAnimationController bossAnimationController;
    public void StartFlameBreath(FlameBreathPatternSO pattern)
    {
        if(flameRoutine != null)
            StopCoroutine(flameRoutine);

        flameRoutine = StartCoroutine(FlameRoutine(pattern));

    }

    private void Awake()
    {
        bossAnimationController = GetComponent<BossAnimationController>();
    }

    private IEnumerator FlameRoutine(FlameBreathPatternSO pattern)
    {
        bossAnimationController.BreathAnim();

        Transform player = GameObject.FindWithTag("Player")?.transform;
        if (player != null)
        {
            Vector3 lookDir = (player.position - transform.position).normalized;
            lookDir.y = 0f;
            transform.forward = lookDir;
        }

        //Vector3 spawnPos = transform.position
        //                 + transform.forward * offset.z
        //                 + transform.up * offset.y
        //                 + transform.right * offset.x;

        GameObject flameVFX = null;
        if(pattern.flameEffectPrefab != null)
        {
            flameVFX = Instantiate(pattern.flameEffectPrefab, spawnPos.position, transform.rotation, transform);
            ParticleSystem ps = flameVFX.GetComponent<ParticleSystem>();
            if (ps) ps.Play();
        }

        float timer = 0f;
        while(timer < pattern.duration)
        {
            if (flameVFX != null)
            {
                flameVFX.transform.position = spawnPos.position;
                flameVFX.transform.rotation = spawnPos.rotation;
                
            }
            
            DealDamageInCone(pattern);
            timer += Time.deltaTime;
            yield return null;
        }
        if (flameVFX != null)
        {
            Destroy(flameVFX);
        }



    }
    private void DealDamageInCone(FlameBreathPatternSO pattern)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pattern.range, pattern.targetMask);
        Vector3 forward = transform.forward;

        foreach (Collider col in hits)
        {
            Vector3 dirToTarget = (col.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(forward, dirToTarget);

            if (angle <= pattern.angle * 0.5f)
            {
                IDamaged damaged = col.GetComponent<IDamaged>();
                if (damaged != null)
                {
                    float damageThisFrame = pattern.damagePerSecond * Time.deltaTime;
                    damaged.TakeDamage(damageThisFrame);

                    Debug.Log($"브레스 히트{col.name}");
                }
            }
        }

    }
}
