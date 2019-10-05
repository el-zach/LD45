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
        mov += Camera.main.transform.right * GetMouseBorderX() * speed * Time.deltaTime;
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = forward.normalized;
        mov += forward * GetMouseBorderY() * speed * Time.deltaTime;
        controlledTarget.Translate(mov);
    }

    float GetMouseBorderX()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log(mousePos);
        if (mousePos.x <= 0.1f)
        {
            return -1f;
        }
        else if (mousePos.x >= 0.9f)
        {
            return 1f;
        }
        else return 0f;
    }

    float GetMouseBorderY()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mousePos.y <= 0.1f)
        {
            return -1f;
        }
        else if (mousePos.y >= 0.9f)
        {
            return 1f;
        }
        else return 0f;
    }


}
