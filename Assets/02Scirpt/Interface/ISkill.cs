using UnityEngine;

public interface ISkill 
{
    string Skillname { get; }
    float SkillCoolDown { get; }
    int ManaCost { get; }

    void Cast(GameObject caster);
}
