using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillBase[] euqippedSkills = new SkillBase[4];

    private float[] cooldownTimer = new float[4];

    Player player;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }

    private void Update()
    {
        HandleInput();
        HandleCoolDowns();
    }

    void TryCastSkill(int index)
    {
        SkillBase skill = euqippedSkills[index];

        if (cooldownTimer[index] > 0f)
        {
            Debug.Log($"{skill.skillName} Äð´Ù¿î Áß : {cooldownTimer}");
        }


        skill.Cast(gameObject);
        
        player.UseMana(skill.manaCost);
        skill.ResetCooldown();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TryCastSkill(0); 
        if (Input.GetKeyDown(KeyCode.W)) TryCastSkill(1); 
        if (Input.GetKeyDown(KeyCode.E)) TryCastSkill(2); 
        if (Input.GetKeyDown(KeyCode.R)) TryCastSkill(3); 
    }

    void HandleCoolDowns()
    {
        for(int i=  0; i<cooldownTimer.Length; i++)
        {
            if(cooldownTimer[i] > 0f)
            {
                cooldownTimer[i] -= Time.deltaTime;
            }
        }
    }
}
