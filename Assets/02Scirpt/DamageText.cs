using System;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText;
    private float floatSpeed = 30f;
    private float duration = 0.5f;

    private float timer;


    public void SetUP(float damage)
    {
        damageText.text = damage.ToString();
        timer = 0f;
        gameObject.SetActive(true);
        SetAlpha(1f);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / duration);
        SetAlpha(alpha);

        if(timer >= duration)
        {
            DamageTextPool.Instance.ReturnToPool(this);
        }

    }


    private void SetAlpha(float alpha)
    {
        Color c = damageText.color;
        c.a = alpha;
        damageText.color = c;
    }
}
