using System.Data.Common;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BossAI : MonoBehaviour, IDamaged
{
    public float dictectionRange = 30f;
    public float disengageRange = 35f;
    [SerializeField] Transform player;

    private bool isEngaged = false;
    private bool isAttacking = false;
    private float attackCooldown = 2f;
    private float cooldownTimer = 0f;

    public float maxHP = 150f;
    public float curHP;
    EventBus eventBus;
    DamageTextSpawner damageTextSpawner;
    BossUIManager bossUIManager;

    private void Awake()
    {
        damageTextSpawner = FindAnyObjectByType<DamageTextSpawner>();
        bossUIManager = FindAnyObjectByType<BossUIManager>();
        eventBus = FindAnyObjectByType<EventBus>();
        curHP = maxHP;
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
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if(!isEngaged && dist < dictectionRange)
        {
            isEngaged = true;
            Debug.Log("플레이어 감지");
        }

        if(isEngaged && dist > disengageRange && !isAttacking)
        {
            isEngaged = false;
            Debug.Log("플레이어 이탈");
        }


        if(isEngaged)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                UseRandomAttackPattern();
                cooldownTimer = attackCooldown;
            }
        }
    }

    void UseRandomAttackPattern()
    {
        int pattern = Random.Range(0, 4);

        switch (pattern)
        {
            case 0:
                Debug.Log("1번 패턴");
                break;
            case 1:
                Debug.Log("2번 패턴");
                break;
            case 2:
                Debug.Log("3번 패턴");
                break;
            case 3:
                Debug.Log("4번 패턴");
                break;

        }

    }
}
