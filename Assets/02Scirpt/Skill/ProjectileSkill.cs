using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

[CreateAssetMenu(fileName = "ProjectileSkill", menuName = "Scriptable Objects/ProjectileSkill")]
public class ProjectileSkill : SkillBase
{
    public GameObject Fireprefabs;
    private float speed = 10f;
    [SerializeField] GameObject HitPrefab;
    Player player;
    PlayerAttack playerAttack;
    float attackRange = 6f;
    AnimatorController animatorController;
    NavMeshAgent agent;

    SkillManager skillManager;
    public override void Cast(GameObject caster, Targetable target)
    {
        Transform firePoint = caster.transform.Find("FirePoint");
        if(player == null )
        {
            player = caster.GetComponent<Player>();
        }
        if (animatorController == null)
        {
            animatorController = caster.GetComponent<AnimatorController>();
        }
        if(playerAttack == null)
        {
            playerAttack = caster.GetComponent<PlayerAttack>();
        }
        
        if(agent == null)
        {
            agent = caster.GetComponent<NavMeshAgent>();
        }
        
        if(skillManager == null)
        {
            skillManager = FindAnyObjectByType<SkillManager>();
        }

        GameObject proj = Instantiate(Fireprefabs, firePoint.position, Quaternion.identity);
        Vector3 dir = (target.GetTargetPoint() - firePoint.position).normalized;

        proj.GetComponent<TargetProjectile>().Init(dir, speed, target.transform, HitPrefab);

        target.OnTargeted();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Targetable curtarget = hit.collider.GetComponent<Targetable>();


            if (target != null)
            {
                float dist = Vector3.Distance(caster.transform.position, target.transform.position);
                if (dist <= attackRange)
                {
                    playerAttack.StopChar();
                    caster.transform.LookAt(target.transform.position);
                    animatorController.SetInt("animation,2");
                }
            }
        }
                
    }
    
}
