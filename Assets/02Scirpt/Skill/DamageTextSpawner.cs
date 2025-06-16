using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] Canvas maincanvas;

    public void ShowDamamge(Vector3 worldPos, float damage)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        DamageText dmgtext = DamageTextPool.Instance.GetFromPool();
        dmgtext.transform.SetParent(maincanvas.transform, false);
        dmgtext.transform.position = screenPos;
        dmgtext.SetUP(damage);
    }
}
