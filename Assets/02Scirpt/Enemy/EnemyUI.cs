using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    Enemy target;
    RectTransform rectTransform;

    [SerializeField] Slider hpSlider;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
        }
    }

    public void init(Enemy enemy)
    {
        target = enemy;

        hpSlider.maxValue = target.maxHP;
        hpSlider.value = target.curHP;

    }

    public void UpdateHP(float hp)
    {
        hpSlider.value = hp;
    }

    public void UpdatePosition(Camera cam)
    {
        if (target == null) return;

        Vector3 pos = cam.WorldToScreenPoint(target.transform.position + Vector3.up * 2);
        transform.position = pos;

        gameObject.SetActive(pos.z > 0);
    }

    
}
