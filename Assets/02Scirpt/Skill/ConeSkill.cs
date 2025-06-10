using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "ConeSkill", menuName = "Scriptable Objects/ConeSkill")]
public class ConeSkill : SkillBase
{
    public float range = 6f;
    public float angle = 60f;
    public LayerMask targetMask;
    [SerializeField] GameObject effectpre;

    public override void Cast(GameObject caster, Targetable target)
    {

        Vector3 origin = caster.transform.position;
        Vector3 forward = caster.transform.forward;

        Vector3 effectPos = origin + forward * 1f + new Vector3(0f, 0.5f, 0f);
        Quaternion rot = Quaternion.LookRotation(forward);
        

        Collider[] hits = Physics.OverlapSphere(origin, range, targetMask);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 Targetdir = hit.point - caster.transform.position;
            Targetdir.y = 0f;
            caster.transform.LookAt(hit.point);

            //origin = caster.transform.position;
            //forward = caster.transform.forward;
            //effectPos = origin + forward * 1f + new Vector3(0f, 0.5f, 0f);
            //rot = Quaternion.LookRotation(forward);
        }

        foreach (Collider hita in hits )
        {
            Vector3 dirToTarget = (hita.transform.position - origin).normalized;
            float angleToTarget = Vector3.Angle(forward, dirToTarget);

            if(angleToTarget < angle / 2f)
            {
                IDamaged t = hita.GetComponent<IDamaged>();
                if (t != null)
                {
                    t.TakeDamage(skillDamage);
                }
            }
        }
        GameObject fx = Instantiate(effectpre, effectPos, rot);
        GameObject.Destroy(fx, 0.7f);

        

        
    }
}
