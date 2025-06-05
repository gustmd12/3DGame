using UnityEngine;

public class Enemy : MonoBehaviour,IDamaged
{
    public float maxHP = 100;
    public float curHP;
    EventBus eventBus;
    EnemyUIManager enemyUIManager;

    private void Awake()
    {
        eventBus = FindAnyObjectByType<EventBus>();
        enemyUIManager = FindAnyObjectByType<EnemyUIManager>();
        curHP = maxHP;
        Debug.Log($"몬스터의 체력 : {curHP}");
    }

    private void Start()
    {
        enemyUIManager = FindAnyObjectByType<EnemyUIManager>();

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
