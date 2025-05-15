using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSkill", menuName = "Scriptable Objects/ProjectileSkill")]
public class ProjectileSkill : SkillBase
{
    public GameObject Fireprefabs;
    private float speed = 10f;

    Player player;

    public override void Cast(GameObject caster)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Targetable target = hit.collider.GetComponent<Targetable>();

            if (target != null)
            {
                Transform firePoint = caster.transform.Find("FirePoint");

                GameObject proj = Instantiate(Fireprefabs, firePoint.position, Quaternion.identity);

                Vector3 dir = (target.GetTargetPoint() - firePoint.position).normalized;


                proj.GetComponent<TargetProjectile>().Init(dir, speed);

                target.OnTargeted();

                ResetCooldown();
            }
        }
    }

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

}
