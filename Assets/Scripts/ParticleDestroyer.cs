using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    ParticleSystem ps;
    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ps != null)
        {
            if (!ps.IsAlive())
            {
                KillParticle();
            }
        }
    }

    public void KillParticle()
    {
        Destroy(gameObject);
    }
}
