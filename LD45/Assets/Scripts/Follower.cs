using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public class LightRating
    {
        public Light light;
        public Vector3 worldDirection;
        public float distance;
        public float score;
    }

    public bool debug = false;
    Movement move;
    public float slowDownDistance = 3f;
    public float maxDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        float topScore = FollowerManager.Instance.minScore;
        LightRating topLight = null;
        foreach(Light light in FollowerManager.allLights)
        {
            //get best Light 
            // -> good light is close and bright

            LightRating rating = new LightRating();
            rating.light = light;
            rating.worldDirection = light.transform.position - transform.position;
            rating.distance = rating.worldDirection.magnitude;
            rating.score = (light.intensity > 0.05f ? light.intensity : 0f) * light.range * (maxDistance - rating.distance);
            if(rating.score > topScore)
            {
                topScore = rating.score;
                topLight = rating;
            }
        }
        if (topLight!=null)
        {
            //for best light
            //check if in range
            //if in range move towards
            //slow down once you reach threshold
            if (CheckLightRange(topLight.light))
            {
                if(debug)Debug.Log("Toplight: Intensity:" + topLight.light.intensity + "; Range: " + topLight.light.range + "; Distance: " + topLight.distance+"; score: "+topLight.score);
                Vector2 dir = new Vector2(topLight.worldDirection.x, topLight.worldDirection.z).normalized;
                dir *= Mathf.Clamp01(topLight.distance / slowDownDistance);
                move.input = dir;
            }
        }
    }

    bool CheckLightRange(Light light)
    {
        return true;
    }

    //Vector2 TargetDirection(Vector3 _targetPos)
    //{
    //    Vector3 dir = _targetPos - transform.position;
    //    return new Vector2(dir.x, dir.z).normalized;
    //}

}
