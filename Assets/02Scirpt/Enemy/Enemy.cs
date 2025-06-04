using UnityEngine;

public class Enemy : MonoBehaviour,IDamaged
{
    public float maxHP = 100;
    public float curHP;
    EventBus eventBus;


    private void Awake()
    {
        eventBus = FindAnyObjectByType<EventBus>();

        curHP = maxHP;
        Debug.Log($"몬스터의 체력 : {curHP}");
    }

    public void TakeDamage(float amount)
    {
        curHP -= amount;

        Debug.Log($"몬스터의 남은 체력 : {curHP}");
        eventBus.OnEnemyHPChanged?.Invoke(this);

        if(curHP < 0 )
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
