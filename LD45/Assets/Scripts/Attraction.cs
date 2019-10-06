using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    public float range, power;
    public Light syncTarget;
    public float multiplier = 1f;

    private void Start()
    {
        if (syncTarget)
        {
            range = syncTarget.range;
            power = syncTarget.intensity;
        }
    }

    private void Update()
    {
        if(syncTarget)
        {
            range = syncTarget.range;
            power = syncTarget.intensity;
        }
    }
}
