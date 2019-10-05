using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticle : MonoBehaviour
{
    public ParticleSystem pSys;
    public LightParticles lightSys;
    [Range(0f,2f)]
    public float lightPower = 0f;
    public Vector2 lightChange = Vector2.one;
    Plane plane;
    public float cooldown = 0.1f;
    float timer = 0f;

    public float currentHeigth = 0f;
    public float baseHeigth = 0.1f;

    Vector2 lastMousePosition=Vector2.zero;
    private void Start()
    {
        plane = new Plane(Vector3.up, currentHeigth+baseHeigth);
    }

    private void Update()
    {
        float mouseDelta = Vector2.Distance(Input.mousePosition, lastMousePosition);
        lastMousePosition = Input.mousePosition;
        if (timer < cooldown) timer += Time.deltaTime;
        if (Input.GetMouseButton(0) && mouseDelta>0f)
        {
            lightPower = Mathf.Lerp(lightPower, 1f, Time.deltaTime * lightChange.x);
            if (timer >= cooldown)
            {
                Vector3 pos = GetMouseWorld();
                DoEmit(pos);
                timer = 0f;
            }
        }
        else
        {
            lightPower = Mathf.Lerp(lightPower, 0f, Time.deltaTime * lightChange.y);
        }
        lightSys.intensityMultiplier = lightPower;
    }

    void DoEmit(Vector3 pos)
    {
        var emit = new ParticleSystem.EmitParams();
        emit.position = pos;
        pSys.Emit(emit, 1);
    }

    public Vector3 GetMouseWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist;
        plane.Raycast(ray, out dist);
        return ray.GetPoint(dist);
    }

}
