using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Setup")]
    public Rigidbody rigid;
    [Header("Stats")]
    public float speed = 1f;
    public bool rotateTowards = true;
    public float rotationSpeed = 1f;
    //public float acceleration=1f;
    //public float velocityDamping = 0.95f;
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
        Vector3 mov = DirectModel();
        if (rotateTowards) ApplyRotation(mov);
    }

    Vector3 DirectModel()
    {
        Vector3 mov = Vector3.forward * input.y + Vector3.right * input.x;
        rigid.MovePosition(rigid.position + mov * speed * Time.deltaTime);
        return mov;
    }

    void AccelerationModel()
    {
        //acc = new Vector3(input.x * acceleration * Time.deltaTime, 0f, input.y * acceleration * Time.deltaTime);
        //vel += acc;

        //if (rotateTowards && vel.sqrMagnitude != 0f) rigid.MoveRotation(Quaternion.LookRotation(vel,Vector3.up));
        //rigid.MovePosition(rigid.position + new Vector3(vel.x, vel.y, vel.z));

        //vel *= velocityDamping;
        //if (vel.sqrMagnitude <= 0.001f)
        //    vel = Vector3.zero;
        //input = Vector2.zero;
        //acc = Vector3.zero;
    }

    void ApplyRotation(Vector3 dir)
    {
        if (dir.sqrMagnitude != 0f) rigid.MoveRotation(Quaternion.Lerp(rigid.rotation, Quaternion.LookRotation(dir, Vector3.up), rotationSpeed*Time.deltaTime));
    }

}
