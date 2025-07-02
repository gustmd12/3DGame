using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private Animator animator;
    private BossFlameBreath bossFlame;

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    public void BreathAnim()
    {
        animator.SetTrigger("Breath");
    }
}
