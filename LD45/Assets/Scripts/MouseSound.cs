using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSound : MonoBehaviour
{
    public AudioClip downClip;

    float lerpSpeed = 2f;
    float vol = 0f;

    AudioSource src;

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
            src.PlayOneShot(downClip);

        vol = Mathf.MoveTowards(vol, Input.GetMouseButton(0) ? 1 : 0, Time.deltaTime * lerpSpeed);
        src.volume = vol;
    }
}
