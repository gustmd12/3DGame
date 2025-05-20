using TMPro;
using UnityEngine;

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
        GameObject fx = Instantiate(effectpre, effectPos, rot);
        GameObject.Destroy(fx, 0.7f);


        Collider[] hits = Physics.OverlapSphere(origin, range, targetMask);

        foreach (Collider hit in hits )
        {
            Vector3 dirToTarget = (hit.transform.position - origin).normalized;
            float angleToTarget = Vector3.Angle(forward, dirToTarget);


            if(angleToTarget < angle / 2f)
            {
                Targetable t = hit.GetComponent<Targetable>();
                if (t != null)
                {
                    Debug.Log("Wµ¥¹ÌÁö");
                }
            }
        }
    }
}
