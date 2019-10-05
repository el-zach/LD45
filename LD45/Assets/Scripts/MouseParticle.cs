using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParticle : MonoBehaviour
{
    public ParticleSystem pSys;

    Plane plane;
    public float cooldown = 0.1f;
    float timer = 0f;

    private void Start()
    {
        plane = new Plane(Vector3.up, 0.1f);
    }

    private void Update()
    {
        if (timer < cooldown) timer += Time.deltaTime;

        if (timer >= cooldown)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = GetMouseWorld();
                DoEmit(pos);
                timer = 0f;
            }
        }
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
