using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanHold : MonoBehaviour
{
    public Transform handBone;
    public GameObject isHolding = null;
    Vector3 itemOriginalPosition;
    Quaternion itemOriginalRotation;


    Vector3 lastLegalPosition;
    private void Update()
    {
        if (isHolding)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit))
                lastLegalPosition = hit.point;
            
        }
    }


    public void AttachToHand(GameObject item)
    {
        if (isHolding)
            return;
        isHolding = item;
        itemOriginalPosition = item.transform.localPosition;
        itemOriginalRotation = item.transform.localRotation;
        item.transform.SetParent(handBone, false);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    public void Detach()
    {
        if (!isHolding) return;

        //find valid position
        //spawn new collectableHolder
        //put isHolding into holder

        GameObject holder = new GameObject();
        holder.transform.position = lastLegalPosition;
        var col = holder.AddComponent<SphereCollider>();
        col.radius = Collectable.TriggerRadius;
        col.isTrigger = true;

        var collectable = holder.AddComponent<Collectable>();
        collectable.itemGraphic = isHolding.transform;

        isHolding.transform.SetParent(holder.transform);
        isHolding.transform.localPosition = itemOriginalPosition;
        isHolding.transform.localRotation = itemOriginalRotation;

        isHolding = null;

    }

}
