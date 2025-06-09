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

    AnimatorController animatorController;

    [SerializeField] GameObject prefabs;
    [SerializeField] Transform firePoint;
    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
        TryGetComponent<PlayerAnimController>(out playerAnimController);
        TryGetComponent<AnimatorController>(out animatorController);
    }
    void Update()
    {
        if (currentTarget == null)
            return;

        float dist = Vector3.Distance(transform.position, currentTarget.position);
        
        if (dist <= attackRange)
        {
            agent.ResetPath();
            transform.LookAt(currentTarget.position);
            

            if (Time.time - lastAttackTime > attackCoolDown)
            {
                Debug.Log("АјАн");
                lastAttackTime = Time.time;

                animatorController.SetInt("animation,5");

                Fire(currentTarget);

                isAttacking = true;
                attackEndTime = Time.time + attackDuration;
                ClearTarget();
            }
        }

    }
    
    public void StopChar()
    {
        agent.ResetPath();
    }

    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    public void ClearTarget()
    {
        currentTarget = null;
    }
    
    public void Fire(Transform target)
    {
        GameObject proj = Instantiate(prefabs, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().initTarget(currentTarget);
    }
}
