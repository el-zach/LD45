using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Follower : MonoBehaviour
{
    public class LightRating
    {
        public Light light;
        public Vector3 worldDirection;
        public float distance;
        public float score;
    }

    public static float killHeigth = -10f;

    public bool debug = false;
    Movement move;
    public float slowDownDistance = 3f;
    public float stoppingDistance = 0.3f;
    public float maxDistance = 10f;

    public AudioClip deathClip;
    public UnityEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        move.speed *= Random.Range(0.9f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < killHeigth)
        {
            Death();
            return;
        }



        float topScore = FollowerManager.Instance.minScore;
        LightRating topLight = null;
        foreach(Attraction attraction in FollowerManager.allLights)
        {
            //get best Light 
            // -> good light is close and bright

            LightRating rating = new LightRating();
            rating.light = attraction.syncTarget;
            rating.worldDirection = attraction.transform.position - transform.position;
            rating.worldDirection.y = 0f;
            rating.distance = rating.worldDirection.magnitude;
            rating.score = (attraction.power > 0.05f ? attraction.power : 0f) * attraction.range * (maxDistance - rating.distance) * attraction.multiplier;
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
                MoveInDirection(topLight);
            }
        }
    }

    bool CheckLightRange(Light light)
    {
        return true;
    }

    void MoveInDirection(LightRating light)
    {
        Vector2 dir = new Vector2(light.worldDirection.x, light.worldDirection.z).normalized;
        dir *= Mathf.Clamp01(light.distance / slowDownDistance - stoppingDistance);
        move.SetInput(dir);
    }

    public void Death()
    {
        Debug.Log(gameObject.name +" died.",gameObject);
        FollowerManager.Instance.GetComponent<AudioSource>().PlayOneShot(deathClip);
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    //Vector2 TargetDirection(Vector3 _targetPos)
    //{
    //    Vector3 dir = _targetPos - transform.position;
    //    return new Vector2(dir.x, dir.z).normalized;
    //}

}
