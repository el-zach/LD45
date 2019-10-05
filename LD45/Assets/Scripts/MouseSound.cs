using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSound : MonoBehaviour
{
    public AudioClip downClip;
    public float mouseVelScale = 0.01f;
    public float lerpSpeed = 1f;

    Vector3 lastMousePosition;

    AudioSource src;
    
    [Header("Output")]
    public float vol = 0f;
    public float mouseVel;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            src.PlayOneShot(downClip);

        mouseVel = (Input.mousePosition - lastMousePosition).magnitude / Time.deltaTime * mouseVelScale;
        lastMousePosition = Input.mousePosition;

        vol = Mathf.MoveTowards(vol, (Input.GetMouseButton(0) ? 0.1f + Mathf.Clamp01(mouseVel) * 0.9f: 0), Time.deltaTime * lerpSpeed);
        src.volume = vol;
    }
}
