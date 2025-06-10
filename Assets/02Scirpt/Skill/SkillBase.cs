using UnityEngine;

[CreateAssetMenu(fileName = "SkillBase", menuName = "Scriptable Objects/SkillBase")]
public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public float cooldown;
    public int manaCost;
    public float skillDamage;
    public SkillType skilltype;

    public enum SkillType
    {
        Target,
        Direction,
        Self,
        Area
    }

    public abstract void Cast(GameObject caster, Targetable target);
   
}
