using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public static FollowerManager Instance;
    public static List<Attraction> allLights = new List<Attraction>();

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public float minScore = 30f;

    // Start is called before the first frame update
    void Start()
    {
        allLights.Clear();
        Attraction[] allLightsAtStart = FindObjectsOfType<Attraction>();
        foreach (Attraction light in allLightsAtStart)
            allLights.Add(light);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
