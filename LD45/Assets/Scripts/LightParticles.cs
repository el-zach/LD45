using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightParticles : MonoBehaviour
{
    [Range(0f,2f)]
    public float intensityMultiplier = 1f;
    public Light leadLight, centerLight;
    public float intensityLead, intensityCenter;
    public ParticleSystem pSys;
    public ParticleSystem.Particle[] pBuffer;

    // Start is called before the first frame update
    void Start()
    {
        pBuffer = new ParticleSystem.Particle[pSys.main.maxParticles];
        intensityLead = leadLight.intensity;
        intensityCenter = centerLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {

        if (pSys.particleCount > 1)
        {
            int activeCount = pSys.GetParticles(pBuffer);
            //find oldest
            //float leastLifetime = 10f;
            float mostLifetime = 0f;
            //int oldest = 0;
            int youngest = 0;
            Vector2 pos= Vector2.zero;
            for (int i = 0; i < activeCount; i++)
            {
                //if (pBuffer[i].remainingLifetime < leastLifetime)
                //{
                //    leastLifetime = pBuffer[i].remainingLifetime;
                //    oldest = i;
                //}
                if (pBuffer[i].remainingLifetime > mostLifetime)
                {
                    mostLifetime = pBuffer[i].remainingLifetime;
                    youngest = i;
                }

                pos.x += pBuffer[i].position.x;
                pos.y += pBuffer[i].position.z;

            }
            pos /= (float)activeCount;
            centerLight.transform.position = new Vector3(pos.x, centerLight.transform.position.y, pos.y);
            //move taillight to oldest
            //tailLight.transform.position = new Vector3(pBuffer[oldest].position.x, tailLight.transform.position.y, pBuffer[oldest].position.z);
            leadLight.transform.position = new Vector3(pBuffer[youngest].position.x, centerLight.transform.position.y, pBuffer[youngest].position.z);
        }

        centerLight.intensity = intensityMultiplier * intensityCenter;
        leadLight.intensity = intensityMultiplier * intensityLead;

    }
}
