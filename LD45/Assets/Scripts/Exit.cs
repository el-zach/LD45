using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    [Header("Setup")]
    public UnityEngine.TextMesh text;

    [Header("Stats")]
    public string activatedText;
    public string readyToLeaveText;

    public UnityEvent OnReadyToLeave= new UnityEvent(), OnNotReadyAnymore = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody?.GetComponent<CanHold>()?.isHolding)
        {
            ChangeTextToReady();
            OnReadyToLeave.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody?.GetComponent<CanHold>()?.isHolding)
        {
            ChangeTextToActivated();
            OnNotReadyAnymore.Invoke();
        }
    }

    void ChangeTextToReady()
    {
        text.text = readyToLeaveText;
    }

    void ChangeTextToActivated()
    {
        text.text = activatedText;
    }

}
