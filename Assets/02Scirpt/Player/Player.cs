using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    public int maxMP = 100;
    public int curHP;
    public int curMP;

    public Action OnManaChanged;

    private void Awake()
    {
        curHP = maxHP;
        curMP = maxMP;
    }

    void Update()
    {
        
    }

    public void UseMana(int amount)
    {
        if (curMP >= amount)
        {
            curMP -= amount;
            OnManaChanged?.Invoke();
        }
        else
        {
            Debug.Log("마나 부족");
        }
            
    }

    
}
