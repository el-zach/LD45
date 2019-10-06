using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public Transform itemGraphic;

    private void OnTriggerEnter(Collider other)
    {
        CanHold hold = other.attachedRigidbody?.GetComponent<CanHold>();
        if (hold)
        {
            hold.AttachToHand(itemGraphic.gameObject);
            Destroy(this.gameObject);
        }
    }
}
