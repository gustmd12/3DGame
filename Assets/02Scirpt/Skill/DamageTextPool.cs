using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : MonoBehaviour
{
    public static DamageTextPool Instance;

    [SerializeField] DamageText damageTextPrefab;
    public int poolSize = 10;

    private Queue<DamageText> pool = new Queue<DamageText>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            DamageText dmgText = Instantiate(damageTextPrefab, transform);
            dmgText.gameObject.SetActive(false);
            pool.Enqueue(dmgText);
        }
    }

    public DamageText GetFromPool()
    {
        if (pool.Count > 0)

        {
            DamageText dmgText = pool.Dequeue();
            dmgText.gameObject.SetActive(true);
            return dmgText;
        }
        else
        {
            DamageText dmgText = Instantiate(damageTextPrefab, transform);
            return dmgText;
        }
    }

    public void ReturnToPool(DamageText dmgText)
    {
        dmgText.gameObject.SetActive(false);
        pool.Enqueue(dmgText);
    }
}
