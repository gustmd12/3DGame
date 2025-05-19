using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileSkill", menuName = "Scriptable Objects/ProjectileSkill")]
public class ProjectileSkill : SkillBase
{
    public GameObject Fireprefabs;
    private float speed = 10f;

    Player player;
    

    public override void Cast(GameObject caster, Targetable target)
    {
        Transform firePoint = caster.transform.Find("FirePoint");

        GameObject proj = Instantiate(Fireprefabs, firePoint.position, Quaternion.identity);
        Vector3 dir = (target.GetTargetPoint() - firePoint.position).normalized;

        proj.GetComponent<TargetProjectile>().Init(dir, speed,target.transform);

        target.OnTargeted();

        Debug.Log("스킬 발동");
        
    }

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

}
