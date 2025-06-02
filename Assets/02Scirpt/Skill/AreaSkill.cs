using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "AreaSkill", menuName = "Scriptable Objects/AreaSkill")]
public class AreaSkill : SkillBase
{
    public float radius = 3f;
    public LayerMask enemyLayer;

    [SerializeField] GameObject HitAreaPre;
    public override void Cast(GameObject caster, Targetable target)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 targetPosition = hit.point;

            Collider[] hits = Physics.OverlapSphere(targetPosition, radius, enemyLayer);

            GameObject proj = Instantiate(HitAreaPre, targetPosition, Quaternion.identity);
            Destroy(proj, 1f);

            Vector3 dir = targetPosition - caster.transform.position;
            dir.y = 0f;
            caster.transform.forward = dir;

            foreach (var hita in hits)
            {
                IDamaged t = hita.GetComponent<IDamaged>();
                if (t != null)
                {
                    Debug.Log("R 스킬 적중");
                    t.TakeDamage(skillDamage);
                }
            }
        }
        
    }
}
