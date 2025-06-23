using UnityEngine;

public class Enemy : MonoBehaviour,IDamaged
{
    public float maxHP = 100;
    public float curHP;
    EventBus eventBus;
    EnemyUIManager enemyUIManager;
    DamageTextSpawner damageTextSpawner;
    EnemyManager enemyManager;
    private void Awake()
    {
        eventBus = FindAnyObjectByType<EventBus>();
        enemyUIManager = FindAnyObjectByType<EnemyUIManager>();
        damageTextSpawner = FindAnyObjectByType<DamageTextSpawner>();
        enemyManager = FindAnyObjectByType<EnemyManager>();

        curHP = maxHP;
        Debug.Log($"몬스터의 체력 : {curHP}");
    }

    private void Start()
    {
        enemyUIManager = FindAnyObjectByType<EnemyUIManager>();

        enemyManager?.enemies.Add(this);

        if( enemyUIManager != null )
        {
            enemyUIManager.Register(this);
        }
    }

    public void TakeDamage(float amount)
    {
        curHP -= amount;

        Debug.Log($"몬스터의 남은 체력 : {curHP}");
        eventBus.OnEnemyHPChanged?.Invoke(this);
        damageTextSpawner.ShowDamage(transform.position + Vector3.up * 2f, amount);

        if(curHP <= 0 )
        {
            eventBus.OnEnemyDied?.Invoke(this);
            Die();
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}
