using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("Setup")]
    public Transform controlledTarget;
    public Vector3 startPosition;
    public Cinemachine.CinemachineVirtualCamera virtualCam;
    Vector3 startZoomOffset;

    [Header("Stats")]
    public float borderSpeed = 6f;
    public float rightClickSpeed = 20f;
    public float zoomStep = 0.2f;
    public float zoomSpeed = 2f;
    public bool invertZoom = true;
    [Range(0.5f,2f)]
    public float currentZoom = 1f;
    float targetZoom = 1f;

    public Bounds bounds;

    Vector3 lastMousePos;
    // Start is called before the first frame update
    void Start()
    {
        startZoomOffset = virtualCam.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset;
        startPosition = controlledTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 mov = Vector3.zero;

        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward = forward.normalized;

        if (!Input.GetMouseButton(1))
        {
            mov += Camera.main.transform.right * GetMouseBorderX(mousePos) * borderSpeed * Time.deltaTime;
            mov += forward * GetMouseBorderY(mousePos) * borderSpeed * Time.deltaTime;
        }
        else
        {
            Vector3 mouseDelta = mousePos - lastMousePos;
            mov += Camera.main.transform.right * mouseDelta.x * rightClickSpeed * Time.deltaTime;
            mov += forward * mouseDelta.y * rightClickSpeed * Time.deltaTime;
        }

        if(bounds.Contains(controlledTarget.position+mov))
            controlledTarget.Translate(mov);

        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0f)
        {
            ChangeZoom(Input.mouseScrollDelta.y * zoomStep * (invertZoom ? -1f : 1f));
        }
        if (!Mathf.Approximately(currentZoom, targetZoom)) DoZoom();

        lastMousePos = mousePos;
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


    void ChangeZoom(float zoomInput)
    {
        targetZoom = Mathf.Clamp(currentZoom + zoomInput, 0.5f, 2f);
    }

    void DoZoom()
    {
        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime*zoomSpeed);
        virtualCam.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset = startZoomOffset * currentZoom;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }

    public void Restart()
    {
        controlledTarget.position = startPosition;
    }

}
