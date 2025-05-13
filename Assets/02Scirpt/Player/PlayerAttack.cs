using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;

public class PlayerAttack : MonoBehaviour
{
    private float attackRange = 5f;
    private float attackCoolDown = 1f;
    private float lastAttackTime = -999;
    private Transform currentTarget;
    private float rotSpeed = 480f;
    private bool isAttacking = false;
    private float attackDuration = 0.5f;
    private float attackEndTime = 0f;
    private NavMeshAgent agent;
    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
    }
    void Update()
    {
        //if (isAttacking)
        //{
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
                Debug.Log("°ø°Ý");
                lastAttackTime = Time.time;

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
    void TargetLookat()
    {
        if (currentTarget == null)
            return;

        Vector3 dir = (currentTarget.position - transform.position).normalized;
        dir.y = 0f;

        if (dir == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        Debug.Log("·è¾Ü È£Ãâ");

    }
}
