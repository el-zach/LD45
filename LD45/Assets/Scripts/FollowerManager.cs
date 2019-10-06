using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public static FollowerManager Instance;
    public static List<Attraction> allLights = new List<Attraction>();

    public GameObject firstFollower;
    public List<GameObject> savedClones = new List<GameObject>();

    public int followerCount;

    public UnityEngine.Events.UnityEvent OnNoMoreFollowers;

    private void OnEnable()
    {
        if (!Instance)
        {
            Instance = this;
            SaveClones(SceneLoader.Instance?.savedObjects);
            followerCount += savedClones.Count;
        }
    }

    public float minScore = 30f;

    // Start is called before the first frame update
    void Start()
    {
        allLights.Clear();
        if(firstFollower) SaveClones(new GameObject[]{firstFollower });
        //Attraction[] allLightsAtStart = FindObjectsOfType<Attraction>();
        //foreach (Attraction light in allLightsAtStart)
        //    allLights.Add(light);
    }

    public void AddAttraction(Attraction attraction)
    {
        allLights.Add(attraction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveClones(GameObject[] toSave)
    {
        if (toSave==null || toSave.Length<1)
            return;
        foreach(var go in toSave)
        {
            GameObject clone = Instantiate(go);
            savedClones.Add(clone);
            clone.SetActive(false);
        }
    }

    public void SpawnClones()
    {
        foreach (var go in savedClones)
        {
            GameObject clone = Instantiate(go);
            clone.SetActive(true);
        }
    }

    public void ReduceFollowers()
    {
        followerCount--;
        if (followerCount == 0)
            OnNoMoreFollowers.Invoke();
    }

}
