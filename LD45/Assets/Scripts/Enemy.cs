using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float damageRange = 3f;
    public float damageForward = 3f;

    [Header("Setup")]
    public Animator animator;
    Follower follow;
    Movement move;

    private void Start()
    {
        follow = GetComponent<Follower>();
        move = GetComponent<Movement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.CompareTag("Follower"))
            Attack(other.attachedRigidbody.transform);
    }

    void Attack(Transform target)
    {
        follow.enabled = false;
        move.SetInput((target.position-transform.position).normalized);
        animator.SetTrigger("Attack");
    }

    public void KillSurrounding()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward * damageForward * transform.localScale.z, damageRange * transform.localScale.z);
        foreach(var hit in hits)
        {
            if (hit.attachedRigidbody && hit.attachedRigidbody.CompareTag("Follower"))
                hit.attachedRigidbody.GetComponent<Follower>().Death();
        }
        follow.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward * damageForward * transform.localScale.z, damageRange * transform.localScale.z);
    }

}
