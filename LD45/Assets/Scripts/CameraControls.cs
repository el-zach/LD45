using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{

    public Transform controlledTarget;
    public float speed = 1f;

    public Bounds bounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 mov = Vector3.zero;
        mov += Camera.main.transform.right * GetMouseBorderX(mousePos) * speed * Time.deltaTime;
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = forward.normalized;
        mov += forward * GetMouseBorderY(mousePos) * speed * Time.deltaTime;
        if(bounds.Contains(controlledTarget.position+mov))
            controlledTarget.Translate(mov);
    }

    float GetMouseBorderX(Vector3 mousePos)
    {
        return GetFloatBorder(mousePos.x);
    }

    float GetMouseBorderY(Vector3 mousePos)
    {
        return GetFloatBorder(mousePos.y);
    }

    float GetFloatBorder(float input)
    {
        if (input <= 0.1f && input > -0.05f)
        {
            return -1f;
        }
        else if (input >= 0.9f && input < 1.05f)
        {
            return 1f;
        }
        else return 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

}
