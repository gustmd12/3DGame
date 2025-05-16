using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
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
                curSkill.Cast(_player);
                playerAnimController.QSkill();

                curTarget = null;
                curSkill = null;
                isMovingtoCast = false;
                curSkillindex = -1;
            }
        }
    }

    void TryCastSkill(int index)
    {
        SkillBase skill = euqippedSkills[index];

        if (cooldownTimer[index] > 0f)
        {
            Debug.Log($"{skill.skillName} 쿨다운 중 : {cooldownTimer}");
            return;
        }


        if(player.curMP < skill.manaCost)
        {
            Debug.Log("마나부족");
            return;
        }

        skill.Cast(_player);
        
        
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

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Targetable target = hit.collider.GetComponent<Targetable>();


            if (target != null)
            {
                float dist = Vector3.Distance(_player.transform.position, target.transform.position);
                if (dist <= attackRange)
                {
                    playerAttack.StopChar();
                    skill.Cast(_player);

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
}
