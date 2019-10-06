using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    [Header("FaceSpawner")]
    public Transform graphicsContainer;
    public Transform headBone;
    public GameObject[] faceVariants;

    // Start is called before the first frame update
    void Start()
    {
        SpawnFace();
    }

    void SpawnFace()
    {
        int rand = Random.Range(0, faceVariants.Length);
        var go = Instantiate(faceVariants[rand], graphicsContainer);
        go.GetComponent<FollowTransform>().target = headBone;
    }
}
