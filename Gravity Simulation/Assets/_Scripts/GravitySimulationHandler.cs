using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySimulationHandler : MonoBehaviour
{
    public int NParticles;
    public Vector2 StartVelocity;
    public Particle[] particles;
    public List<Transform> TransformParticles;
    public GameObject prefabParticle;
    private void Start()
    {
        particles = ParticleSpawner.instance.LinearSpreadSpawn(NParticles, StartVelocity);
        TransformParticles = new List<Transform>();
        for (int i = 0; i < particles.Length; i++)
        {
            TransformParticles.Add(Instantiate(prefabParticle, particles[i].Position, Quaternion.identity, transform).transform);
        }
    }
    private void Update()
    {
        SimulateGravity();
    }
    public void SimulateGravity()
    {
        for (int i = 0; i < NParticles; i++)
        {
            particles[i].Position += particles[i].Velocity * Time.deltaTime; //calculates the position of the current cirlce
            TransformParticles[i].position = particles[i].Position;
        }
    }
}