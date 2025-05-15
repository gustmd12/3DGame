using UnityEngine;

[CreateAssetMenu(fileName = "SkillBase", menuName = "Scriptable Objects/SkillBase")]
public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    public float cooldown;
    public int manaCost;

    private float currentCooldown;

    private float Cooldown => cooldown;

    public abstract void Cast(GameObject caster);

    public void UpdateCooldown()
    {
        if(currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    public bool IsCooldownComplete()
    {
        return currentCooldown <= 0f;
    }

    public void ResetCooldown()
    {
        currentCooldown = cooldown;
    }

    public bool CanUseSkill(int currentMana)
    {
        return currentMana >= manaCost;
    }
}
