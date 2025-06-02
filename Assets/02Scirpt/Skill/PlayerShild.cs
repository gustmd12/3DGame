using UnityEngine;

public class PlayerShild : MonoBehaviour
{
    private float currentShild;
    private float shildTimer;

    GameObject shieldpre;

    GameObject player;

    UIManager uIManager;

    private void Awake()
    {
        uIManager = FindAnyObjectByType<UIManager>();
    }

    private void Update()
    {
        CheckShild();
    }

    public void CheckShild()
    {
        if (currentShild > 0f)
        {
            shildTimer -= Time.deltaTime;
            if (shildTimer <= 0f)
            {
                currentShild = 0f;
                Destroy(shieldpre);
                Debug.Log("방어막 해제");

            }
        }
    }

    public void ApplyShild(float amount, float shildTime)
    {

        currentShild = amount;
        shildTimer = shildTime;
        uIManager.UpdateShield(amount);
        //Debug.Log($"방어막 : {amount} 지속시간 : {shildTime}");
    }

    public void Initpre(GameObject shild)
    {
        shieldpre = shild;
    }

    public float AbsorbDamage(float damage)
    {
        if(currentShild > 0f)
        {
            float absorbed = Mathf.Min(damage, currentShild);
            currentShild -= absorbed;

            if(currentShild <= 0f)
            {
                Destroy(shieldpre);
            }

            return damage - absorbed;
        }

        return damage;
    }
}
