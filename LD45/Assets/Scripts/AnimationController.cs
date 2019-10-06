using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        GetComponent<Movement>().OnInput.AddListener(SetSpeedValue);
    }

    private void Update()
    {
        
    }

    void SetSpeedValue(Vector2 _in)
    {
        animator.SetFloat("speed", _in.magnitude);
    }

}
