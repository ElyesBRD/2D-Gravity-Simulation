using System.Collections.Generic;
using UnityEngine;

public class GravitySimulationHandler : MonoBehaviour
{
    public static GravitySimulationHandler instance;
    public Particle[] particles;

    public int NParticles;
    public Vector2 StartVelocity;
    public float Gravitational_constant = 1;
    public float BorderSize;
    public float MinVelocityToFreez = 2;
    public float MinimumDistanceBetweenCenters = 0.1f;

    public List<Transform> TransformParticles;
    public GameObject prefabParticle;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
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
        for (int i = 0; i < particles.Length; i++)
        {
            //CalculateParticlesVelicities(i);
            particles[i].Position += particles[i].Velocity * Time.deltaTime; //calculates the position of the current cirlce
            particles[i].Velocity += particles[i].Acceleration * Time.deltaTime; //calculates the velocity of the current circle
            TransformParticles[i].position = particles[i].Position;
            ContinousWallsCollisionDetection(i);
        }
        if (particles.Length >=2)BarnesHutAlgorithm.CreateBarnesHutTree(particles, -BorderSize, BorderSize);
    }
    public void CalculateParticlesVelicities(Particle[] particles ,int i)
    {
        for (int j = i + 1; j < particles.Length; j++)
        {
            if (Vector3.Distance(particles[i].Position, particles[j].Position) < MinimumDistanceBetweenCenters) return;

            Vector3 direction = particles[j].Position - particles[i].Position;
            float distance = direction.magnitude;

            float force = (Gravitational_constant * particles[i].Mass * particles[j].Mass) / (distance * distance);
            Vector3 forceVector = force * direction.normalized;

            // Apply force to particles based on their masses
            particles[i].Acceleration += Vector2.one * forceVector / particles[i].Mass;
            particles[j].Acceleration -= Vector2.one * forceVector / particles[j].Mass;
        }
    }
    public void ContinousWallsCollisionDetection(int i)
    {
        //calculate circle position in the next frame
        float NextXPos = particles[i].Position.x + particles[i].Velocity.x * Time.deltaTime;
        float NextYPos = particles[i].Position.y + particles[i].Velocity.y * Time.deltaTime;
        //checks if the next position is outside the bounding box or inside
        //check Up and Bottm
        if (NextYPos > BorderSize - 1)
        {
            particles[i].Position.y = (BorderSize - 1) - ((NextYPos) - (BorderSize - 1));
            if (particles[i].Velocity.magnitude < MinVelocityToFreez) particles[i].Velocity = Vector2.zero;
            else particles[i].Velocity.y *= -1;
        }
        //check Bottom
        if (NextYPos < -BorderSize + 1)
        {
            particles[i].Position.y = (-BorderSize + 1) - ((NextYPos) - (-BorderSize + 1));
            if (particles[i].Velocity.magnitude < MinVelocityToFreez) particles[i].Velocity = Vector2.zero;
            else particles[i].Velocity.y *= -1;
        }
        //check Right and Left
        if (NextXPos > BorderSize - 1)
        {
            particles[i].Position.x = (BorderSize - 1) - ((NextXPos) - (BorderSize - 1));
            if (particles[i].Velocity.magnitude < MinVelocityToFreez) particles[i].Velocity = Vector2.zero;
            else particles[i].Velocity.x *= -1;
        }
        //check Left
        if (NextXPos < -BorderSize + 1)
        {
            particles[i].Position.x = (-BorderSize + 1) - ((NextXPos) - (-BorderSize + 1));
            if (particles[i].Velocity.magnitude < MinVelocityToFreez) particles[i].Velocity = Vector2.zero;
            else particles[i].Velocity.x *= -1;
        }
    }
    public void UpdateParticlesArray(Particle[] newArray)
    {
        particles = new Particle[newArray.Length];
        particles = newArray;
        for (int i = particles.Length - 1; i < newArray.Length; i++)
        {
            TransformParticles.Add(Instantiate(prefabParticle, particles[i].Position, Quaternion.identity, transform).transform);
            TransformParticles[i].GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}