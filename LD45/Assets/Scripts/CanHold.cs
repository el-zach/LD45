using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanHold : MonoBehaviour
{
    public Transform handBone;
    public GameObject isHolding = null;
    Vector3 itemOriginalPosition;
    Quaternion itemOriginalRotation;

    public UnityEvent OnPickUp;

    Vector3 lastLegalPosition;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        FollowerManager.Instance.followerCount++;
    }

    private void OnDestroy()
    {
        FollowerManager.Instance.ReduceFollowers();
    }

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
        item.transform.SetParent(handBone, true);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        OnPickUp.Invoke();
        //item.transform.localScale = Vector3.one;
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

        isHolding.transform.SetParent(holder.transform,true);
        isHolding.transform.localPosition = itemOriginalPosition;
        isHolding.transform.localRotation = itemOriginalRotation;
        isHolding.transform.localScale = Vector3.one;

        isHolding = null;

    }

}
