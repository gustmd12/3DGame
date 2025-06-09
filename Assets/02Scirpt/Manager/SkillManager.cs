using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{

    public SkillBase[] euqippedSkills = new SkillBase[4];

    private float[] cooldownTimer = new float[4];

    Player player;

    GameObject _player;

    PlayerAttack playerAttack;
    PlayerMovement playerMovement;
    NavMeshAgent agent;

    PlayerAnimController playerAnimController;

    float attackRange = 6f;

    private Targetable curTarget;
    private SkillBase curSkill;
    private int curSkillindex;
    private bool isMovingtoCast;

    [SerializeField] SkillCooldown skillCooldown;

    UIManager uimanager;

    CharAnimManager charAnimManager;
    AnimatorController animatorController;
    
    private Dictionary<KeyCode, SkillSlot> keySkillmap = new Dictionary<KeyCode, SkillSlot>
    {
        {KeyCode.Q, SkillSlot.Slot1},
        {KeyCode.W, SkillSlot.Slot2},
        {KeyCode.E, SkillSlot.Slot3},
        {KeyCode.R, SkillSlot.Slot4}

    };

    private void HandleInput()
    {
        foreach (var entry in keySkillmap)
        {
            if(Input.GetKeyDown(entry.Key))
            {
                SkillSlot skillSlot = entry.Value;
                int skillIndex = (int)skillSlot;

                SkillBase skill = euqippedSkills[skillIndex];

                if (cooldownTimer[skillIndex] > 0f || player.curMP < skill.manaCost)
                {
                    return;
                }

                    switch (skill.skilltype)
                {
                    case SkillBase.SkillType.Target:
                        TargetSkill(skillIndex);
                        break;
                    case SkillBase.SkillType.Direction:
                        TryCastSkill(skillIndex);
                        animatorController.SetInt("animation,3");
                        break;
                    case SkillBase.SkillType.Self:
                        TryCastSkill(skillIndex);
                        break;
                    case SkillBase.SkillType.Area:
                        animatorController.SetInt("animation,4");
                        //TryCastSkill(skillIndex);
                        break;
                    default:
                        Debug.Log("알수없는 스킬타입");
                        break;
                }

            }
        }
    }


    private void Awake()
    {
        uimanager = FindAnyObjectByType<UIManager>();
        _player = GameObject.Find("Player");
        player = _player.GetComponent<Player>();
        playerAttack = _player.GetComponent<PlayerAttack>();
        playerMovement = _player.GetComponent<PlayerMovement>();
        agent = _player.GetComponent<NavMeshAgent>();
        playerAnimController = _player.GetComponent<PlayerAnimController>();

        charAnimManager = _player.GetComponent<CharAnimManager>();
        animatorController = _player.GetComponent<AnimatorController>();
    }

    private void Start()
    {
        for(int i = 0; i < euqippedSkills.Length; i++)
        {
            uimanager.SetMana(i,euqippedSkills[i]);
        }


    }

    private void Update()
    {
        HandleInput();
        HandleCoolDowns();

        if(isMovingtoCast && curTarget != null && curSkill != null)
        {
            float dist = Vector3.Distance(_player.transform.position, curTarget.transform.position);
            if(dist <= attackRange)
            {
                agent.ResetPath();
                
                player.UseMana(curSkill.manaCost);
                cooldownTimer[curSkillindex] = curSkill.cooldown;
                curSkill.Cast(_player, curTarget);
                animatorController.SetInt("animation,2");


                SetSKill();
            }
        }
    }

    void SetSKill()
    {
        curTarget = null;
        curSkill = null;
        isMovingtoCast = false;
        curSkillindex = -1;
    }

    public void TryCastSkill(int index)
    {
        SkillBase skill = euqippedSkills[index];

        if (cooldownTimer[index] > 0f)
        {
            Debug.Log($"{skill.skillName} 쿨다운 중 : {cooldownTimer[index]:F2}");
            return;
        }


        if(player.curMP < skill.manaCost)
        {
            Debug.Log("마나부족");
            return;
        }

        skill.Cast(_player, null);
        playerAttack.StopChar();

        player.UseMana(skill.manaCost);
        cooldownTimer[index] = skill.cooldown;
        
    }

    void TargetSkill(int index)
    {

        SkillBase skill = euqippedSkills[index];

        if (cooldownTimer[index] > 0f)
        {
            Debug.Log($"{skill.skillName} 쿨다운 중 : {cooldownTimer[index]:F2}");
            return;
        }


        if (player.curMP < skill.manaCost)
        {
            Debug.Log("마나부족");
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;
        
        
        var target = hit.collider.GetComponent<Targetable>();
        if (target == null)
        {
            Debug.Log("타겟이 없습니다");
            return;
        }

        float dist = Vector3.Distance(_player.transform.position, target.transform.position);

        if (dist <= attackRange)
        {
            player.UseMana(skill.manaCost);
            cooldownTimer[index] = skill.cooldown;
            skill.Cast(_player, target);
        }
        else
        {
            curTarget = target;
            curSkill = skill;
            curSkillindex = index;
            isMovingtoCast = true;
            MoveCast(target, attackRange);
        }

    }
    public void MoveCast(Targetable target, float attackRange)
    {
        float dist = Vector3.Distance(_player.transform.position, target.transform.position);
        if (dist >= attackRange)
        {
            agent.SetDestination(target.transform.position);
            
        }
    }

    void HandleCoolDowns()
    {
        for(int i=  0; i<cooldownTimer.Length; i++)
        {
            if(cooldownTimer[i] > 0f)
            {
                cooldownTimer[i] -= Time.deltaTime;

                if (cooldownTimer[i] < 0f)
                    cooldownTimer[i] = 0f;

                float ratio = cooldownTimer[i] / euqippedSkills[i].cooldown;
                float remaing = cooldownTimer[i];
                skillCooldown.CoolDownUpdate(i, ratio, remaing);
            }
            else
            {
                skillCooldown.CoolDownUpdate(i, 0f, cooldownTimer[i]);
            }
        }
    }

}
