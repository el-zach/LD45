using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public Transform controlledTarget;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mov = Vector3.zero;
        mov += Camera.main.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = forward.normalized;
        mov += forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        controlledTarget.Translate(mov);
    }
}
