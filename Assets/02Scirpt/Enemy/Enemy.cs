using UnityEngine;

public class Enemy : MonoBehaviour,IDamaged
{
    private float maxHP = 100;
    private float curHP;

    private void Awake()
    {
        curHP = maxHP;
        Debug.Log($"몬스터의 체력 : {curHP}");
    }

    public void TakeDamage(float amount)
    {
        curHP -= amount;

        if(curHP < 0 )
        {
            Die();
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}
