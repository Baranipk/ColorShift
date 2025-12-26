using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void SetAnimationWalk()
    {
        animator.SetBool("IsFall", false);
        animator.SetBool("IsWalk", true);
    }

    public void SetAnimationIdle()
    {
        animator.SetBool("IsFall", false);
        animator.SetBool("IsWalk", false);
    }

    public void SetAnimationJump()
    {
        animator.SetTrigger("Jump");
        animator.SetBool("IsFall", false);
    }

    public void SetAnimationFall()
    {
        animator.SetBool("IsFall", true);
    }
}
