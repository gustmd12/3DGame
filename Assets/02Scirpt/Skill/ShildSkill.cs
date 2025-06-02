using UnityEngine;

[CreateAssetMenu(fileName = "ShildSkill", menuName = "Scriptable Objects/ShildSkill")]
public class ShildSkill : SkillBase
{
    private float shildAmount = 30f;
    private float duration = 5f;

    [SerializeField] GameObject shildPre;
    public override void Cast(GameObject caster, Targetable target)
    {
        PlayerShild playerShild = caster.GetComponent<PlayerShild>();
        if (playerShild != null)
        {
            playerShild.ApplyShild(shildAmount, duration);


            Debug.Log($"방어막 : {shildAmount}, 지속시간 : {duration}");
        }

        if(shildPre != null)
        {
            Quaternion shildRot = Quaternion.Euler(-90, 0, 0);

            GameObject shield = Instantiate(shildPre, caster.transform.position, shildRot);
            playerShild.Initpre(shield);
            shield.transform.SetParent(caster.transform);

        }
    }
}
