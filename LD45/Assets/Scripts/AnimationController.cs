using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    Movement move;

    private void Start()
    {
        //GetComponent<Movement>().OnInput.AddListener(SetSpeedValue);
        move = GetComponent<Movement>();
    }

    private void Update()
    {
        SetSpeedValue(move.GetInput());
    }

    void SetSpeedValue(Vector2 _in)
    {
        animator.SetFloat("speed", _in.magnitude);
    }

}
