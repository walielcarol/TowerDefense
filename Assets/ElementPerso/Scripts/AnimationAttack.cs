using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttack : MonoBehaviour
{
    public Animator animator;

    public void SetAttack()
    {
        animator.SetBool("IsAttacking", true);
       
    }
    public void SetIdle()
    {
        animator.SetBool("IsAttacking", false);

    }
}
