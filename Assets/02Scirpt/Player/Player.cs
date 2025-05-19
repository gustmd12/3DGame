using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP = 100;
    public int maxMP = 100;
    public int curHP;
    public int curMP;


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
            
        }
        else
        {
            Debug.Log("마나 부족");
        }
            
    }
}
