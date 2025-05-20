using UnityEngine;

public class PlayerShild : MonoBehaviour
{
    private float currentShild;
    private float shildTimer;

    GameObject shieldpre;

    GameObject player;
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
        //Debug.Log($"방어막 : {amount} 지속시간 : {shildTime}");
    }

    public void Initpre(GameObject shild)
    {
        shieldpre = shild;
    }
}
