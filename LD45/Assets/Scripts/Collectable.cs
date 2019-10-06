using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public static float TriggerRadius;
    private void Awake()
    {
        if (TriggerRadius == 0f) TriggerRadius = GetComponent<SphereCollider>().radius;
    }

    public Transform itemGraphic;
    public float rotationSpeed = 1f;

    public UnityEvent OnCollected = new UnityEvent();

    private void Update()
    {
        itemGraphic.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        CanHold hold = other.attachedRigidbody?.GetComponent<CanHold>();
        if (hold)
        {
            hold.AttachToHand(itemGraphic.gameObject);
            OnCollected.Invoke();
            Destroy(this.gameObject);
        }
    }
}
