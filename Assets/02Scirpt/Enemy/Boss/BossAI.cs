using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BossAI : MonoBehaviour, IDamaged
{
    public float dictectionRange = 60f;
    public float disengageRange = 35f;
    [SerializeField] Transform player;

    private bool isEngaged = false;
    private bool isAttacking = false;
    private float attackCooldown = 2f;
    private float cooldownTimer = 0f;

    private float flameCooldown = 6f;
    private float flameTimer = 0f;

    public float maxHP = 150f;
    public float curHP;

    private int currentPatternIndex = 0;
    private float patternTimer = 0;

    EventBus eventBus;
    DamageTextSpawner damageTextSpawner;
    BossUIManager bossUIManager;

    BossAnimationController bossAnimationController;

    private BossPatternExecutor executor;
    private float timer;
    //[SerializeField] private FlameBreathPatternSO flameBreathPattern;
    [SerializeField] private List<BossPatternSO> patternList;
    private BossPatternSO lastUsedPattern = null;


    private void Awake()
    {
        damageTextSpawner = FindAnyObjectByType<DamageTextSpawner>();
        bossUIManager = FindAnyObjectByType<BossUIManager>();
        eventBus = FindAnyObjectByType<EventBus>();
        bossAnimationController = GetComponent<BossAnimationController>();

        curHP = maxHP;

        executor = GetComponent<BossPatternExecutor>();
    }

    private void Start()
    {
        if(bossUIManager != null)
        {
            bossUIManager.Register(this);
        }
    }
    public void TakeDamage(float amount)
    {
        curHP -= amount;

        Debug.Log($"몬스터의 남은 체력 : {curHP}");
        eventBus.OnBossHPChanged?.Invoke(this);
        damageTextSpawner.ShowDamage(transform.position + Vector3.up * 2f, amount);

        if (curHP <= 0)
        {
            eventBus.OnBossDied?.Invoke(this);
            StartCoroutine(Die()); 
        }
    }

    private IEnumerator Die()
    {
        isEngaged = false;
        isAttacking = false;
        bossAnimationController.DieAnim();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if(!isEngaged && dist < dictectionRange)
        {
            isEngaged = true;
            Debug.Log("플레이어 감지");
            //ExecuteRandomPattern();
            patternTimer = 0f;
        }

        if(isEngaged && dist > disengageRange && !isAttacking)
        {
            isEngaged = false;
            Debug.Log("플레이어 이탈");
        }


        if (isEngaged)
        {
            patternTimer -= Time.deltaTime;

            if (patternTimer <= 0f)
            {
                ExecuteRandomPattern();
                flameTimer = flameCooldown;
            }
        }

    }


    private void TryExecutePattern()
    {
        if (patternList == null || patternList.Count == 0) return;

        for(int i = 0; i < patternList.Count; i++)
        {
            int index = (currentPatternIndex + i) % patternList.Count;
            var pattern = patternList[index];

            bool hpCondition = pattern.triggerHPPercent <= 0f || curHP <= maxHP * pattern.triggerHPPercent;

            if (hpCondition)
            {
                executor.Execute(pattern);
                patternTimer = pattern.coolDown;
                currentPatternIndex = (index + 1) % patternList.Count;
                return;
            }
        }
    }
    
    private void ExecuteRandomPattern()
    {

        if (patternList == null || patternList.Count == 0) return;

        List<BossPatternSO> usablePatterns = new List<BossPatternSO> ();
        foreach(var pattern in patternList)
        {
            bool hpCondition = pattern.triggerHPPercent <= 0f || curHP <= maxHP * pattern.triggerHPPercent;

            if(hpCondition && pattern != lastUsedPattern)
            {
                usablePatterns.Add(pattern);
            }

        }

        if (usablePatterns.Count == 0)
        {
            usablePatterns.AddRange(patternList.FindAll(p =>
                p.triggerHPPercent <= 0f || curHP <= maxHP * p.triggerHPPercent));
        }
        
        var selected = usablePatterns[Random.Range(0, usablePatterns.Count)];

        executor.Execute(selected);
        patternTimer = selected.coolDown;

        lastUsedPattern = selected;
    }
}
