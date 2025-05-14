using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class PlayerAttack : MonoBehaviour
{
    private float attackRange = 5f;
    private float attackCoolDown = 1f;
    private float lastAttackTime = -999f;
    private Transform currentTarget;
    private float rotSpeed = 480f;
    private bool isAttacking = false;
    private float attackDuration = 0.5f;
    private float attackEndTime = 0f;
    private NavMeshAgent agent;

    private PlayerAnimController playerAnimController;

    [SerializeField] GameObject prefabs;
    [SerializeField] Transform firePoint;
    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
        TryGetComponent<PlayerAnimController>(out playerAnimController);
    }
    void Update()
    {
        //if (isAttacking)
        //{서서히 돌아가 타겟 쳐다보기
        //    TargetLookat();

        //    if(Time.time >= attackEndTime)
        //    {
        //        ClearTarget();
        //        isAttacking = false;
                
        //    }

        //    return;
        //}
        
        if (currentTarget == null)
            return;

        float dist = Vector3.Distance(transform.position, currentTarget.position);
        
        if (dist <= attackRange)
        {
            agent.ResetPath();
            transform.LookAt(currentTarget.position);
            //TargetLookat();

            if (Time.time - lastAttackTime > attackCoolDown)
            {
                Debug.Log("공격");
                lastAttackTime = Time.time;
                
                playerAnimController.Attack();
                Fire(currentTarget);

                isAttacking = true;
                attackEndTime = Time.time + attackDuration;
                ClearTarget();
            }
        }

    }
    
    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    public void ClearTarget()
    {
        currentTarget = null;
    }
    //void TargetLookat()
    //{
    //    if (currentTarget == null)
    //        return;

    //    Vector3 dir = (currentTarget.position - transform.position).normalized;
    //    dir.y = 0f;

    //    if (dir == Vector3.zero) return;

    //    Quaternion targetRotation = Quaternion.LookRotation(dir);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    //    Debug.Log("룩앳 호출");
    //}

    public void Fire(Transform target)
    {
        GameObject proj = Instantiate(prefabs, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().initTarget(currentTarget);
    }
}
