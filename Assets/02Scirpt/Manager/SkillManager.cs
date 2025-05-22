using System;
using System.Collections;
using System.Collections.Generic;
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


    private void Awake()
    {
        
        _player = GameObject.Find("Player");
        player = _player.GetComponent<Player>();
        playerAttack = _player.GetComponent<PlayerAttack>();
        playerMovement = _player.GetComponent<PlayerMovement>();
        agent = _player.GetComponent<NavMeshAgent>();
        playerAnimController = _player.GetComponent<PlayerAnimController>();
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
                playerAnimController.QSkill();


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

    void TryCastSkill(int index)
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

        if(index == 1)
        {// 스킬시전 방향 바라보기
            playerAnimController.WSkill();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 Targetdir = hit.point - _player.transform.position;
                Targetdir.y = 0f;
                _player.transform.LookAt(hit.point);
            }
        }

        if(index == 3)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Vector3 targetPosition = hit.point;

                AreaSkill areaSkill = skill as AreaSkill;
                if (areaSkill != null)
                {
                    Vector3 dir = targetPosition - _player.transform.position;
                    dir.y = 0f;
                    _player.transform.forward = dir;
                    StartCoroutine(delaySkill(areaSkill, targetPosition));
                    //areaSkill.CastatPosition(_player, targetPosition);
                    //StartCoroutine(HideRangeCoroutine(areaSkill, targetPosition));
                    playerAnimController.RSkill();
                    agent.isStopped = true;

                    Debug.Log("R스킬 시전");
                }
            }
            player.UseMana(skill.manaCost);
            cooldownTimer[index] = skill.cooldown;
            return;
        }


        skill.Cast(_player,null);
        playerAttack.StopChar();
        
        player.UseMana(skill.manaCost);
        cooldownTimer[index] = skill.cooldown;
        
        
    }

    private IEnumerator delaySkill(AreaSkill areaSkill, Vector3 targetPosition)
    {
        yield return new WaitForSeconds(0.5f);
        areaSkill.CastatPosition(_player, targetPosition);
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
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

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Targetable target = hit.collider.GetComponent<Targetable>();


            if (target != null)
            {
                float dist = Vector3.Distance(_player.transform.position, target.transform.position);
                if (dist <= attackRange)
                {
                    playerAttack.StopChar();
                    skill.Cast(_player,target);

                    _player.transform.LookAt(target.transform.position);
                    playerAnimController.QSkill();

                    player.UseMana(skill.manaCost);
                    cooldownTimer[index] = skill.cooldown;
                    
                }
                else
                {
                    curTarget = target;
                    curSkill = skill;
                    curSkillindex = index;
                    isMovingtoCast = true;
                    MoveCast(target,attackRange);
                    
                }
            }
            else
            {
                Debug.Log("타겟이 없습니다");
            }
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

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TargetSkill(0); 
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

    //private IEnumerator HideRangeCoroutine(AreaSkill areaSkill, Vector3 targetPosition)
    //{
    //    yield return areaSkill.HideRange(_player, targetPosition);
    //}
}
