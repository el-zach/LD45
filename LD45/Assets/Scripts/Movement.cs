using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Setup")]
    public Rigidbody rigid;
    [Header("Stats")]
    public bool rotateTowards = true;
    public float acceleration=1f;
    public float velocityDamping = 0.95f;
    //public float gravity = 9.81f;
    [Header("Runtime")]
    public Vector2 input;
    Vector3 acc=Vector3.zero;
    Vector3 vel=Vector3.zero;

    private void Start()
    {
        if (!rigid) GetComponent<Rigidbody>();
    }

    private void Update()
    {
        acc = new Vector3(input.x * acceleration * Time.deltaTime, 0f, input.y * acceleration * Time.deltaTime);
        vel += acc;
        //vel += Vector3.down * gravity*Time.deltaTime;
        if (rotateTowards && vel.sqrMagnitude != 0f) rigid.MoveRotation(Quaternion.LookRotation(vel,Vector3.up));
        rigid.MovePosition(rigid.position + new Vector3(vel.x, vel.y, vel.z));

        vel *= velocityDamping;
        if (vel.sqrMagnitude <= 0.001f)
            vel = Vector3.zero;
        input = Vector2.zero;
        acc = Vector3.zero;
    }

}
