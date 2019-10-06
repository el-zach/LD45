using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Exit : MonoBehaviour
{
    [Header("Setup")]
    public UnityEngine.TextMesh text;
    BoxCollider boxTrigger;

    [Header("Stats")]
    public string activatedText;
    public string readyToLeaveText;

    public int nextSceneIndex = 0;

    public UnityEvent OnReadyToLeave= new UnityEvent(), OnNotReadyAnymore = new UnityEvent();

    private void Start()
    {
        boxTrigger = GetComponent<BoxCollider>();
    }

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

    public void NextLevel()
    {
        Collider[] toSave = Physics.OverlapBox(transform.position + boxTrigger.center, boxTrigger.size);
        List<GameObject> followers = new List<GameObject>();

        foreach(var col in toSave)
        {
            if (col.attachedRigidbody && col.attachedRigidbody.CompareTag("Follower"))
            {
                followers.Add(col.attachedRigidbody.gameObject);
            }
        }

        SceneLoader.Instance.TransportGameObjects(followers.ToArray(), transform.position);
        SceneLoader.Instance.LoadScene(nextSceneIndex);
    }

}
