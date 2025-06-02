using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHP = 100;
    public float maxMP = 100;
    public float curHP;
    public float curMP;

    EventBus eventBus;

    public int manaRegen = 2;

    private void Awake()
    {
        eventBus = FindAnyObjectByType<EventBus>();

        curHP = maxHP;
        curMP = maxMP;
    }

    void Update()
    {
        RegenMana();
    }

    void RegenMana()
    {
        if(curMP < maxMP)
        {
            curMP += manaRegen * Time.deltaTime;
            curMP = Mathf.Min(curMP, maxMP);
            eventBus.OnManaChanged?.Invoke();
        }
    }

    public void UseMana(int amount)
    {
        if (curMP >= amount)
        {
            curMP -= amount;
            eventBus.OnManaChanged?.Invoke();
        }
        else
        {
            Debug.Log("마나 부족");
        }
            
    }

    
}
