using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        TryGetComponent<Animator>(out animator);
    }

    public void PlayerMove(bool isMoving)
    {
        animator.SetBool("Move", isMoving);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void QSkill()
    {
        animator.SetTrigger("QSkill");
    }
}
