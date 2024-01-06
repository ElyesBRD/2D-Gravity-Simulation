using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    public Particle[] LinearSpreadSpawn(int NParticles, Vector2 StartVelocity)
    {
        Particle[] newParticles = new Particle[NParticles];
        for (int i = 0; i < NParticles; i++)
        {
            newParticles[i] = new Particle(new Vector2(2f * i, 2f * i), new Vector2(UnityEngine.Random.Range(-StartVelocity.x, StartVelocity.x), UnityEngine.Random.Range(-StartVelocity.y, StartVelocity.y)), 1);
        }
        return newParticles;
    }
}