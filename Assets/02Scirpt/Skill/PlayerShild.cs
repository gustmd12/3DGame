using Unity.VisualScripting;
using UnityEngine;

public class PlayerShild : MonoBehaviour
{
    public float currentShild;
    private float shildTimer;

    GameObject shieldpre;

    GameObject player;

    UIManager uIManager;

    EventBus eventBus;
    private void Awake()
    {
        uIManager = FindAnyObjectByType<UIManager>();
        eventBus = FindAnyObjectByType<EventBus>();
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
                uIManager?.UpdateShield(0f);
            }
        }
    }

    public void ApplyShild(float amount, float shildTime)
    {
        if (currentShild > 0f) return;

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
        if (currentShild <= 0f)
            return damage;

        float absorbed = Mathf.Min(damage, currentShild);
        currentShild -= absorbed;

        Debug.Log($"쉴드가 {absorbed} 데미지를 흡수, 남은 쉴드: {currentShild}");
        eventBus.OnPlayerShieldChanged?.Invoke(currentShild);
        if (currentShild <= 0f && shieldpre != null)
        {
            Destroy(shieldpre);
            uIManager.DestroyShield();
            Debug.Log("쉴드 파괴");
        }

        return damage - absorbed; 
    }
}
