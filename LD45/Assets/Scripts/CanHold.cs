using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHold : MonoBehaviour
{
    public Transform handBone;
    public GameObject isHolding = null;
    public void AttachToHand(GameObject item)
    {
        if (isHolding)
            return;
        isHolding = item;
        item.transform.SetParent(handBone, false);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

}
