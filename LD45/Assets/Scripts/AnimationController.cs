using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    Rigidbody rigid;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        animator.SetFloat("speed", rigid.velocity.magnitude);
    }

}
