using System.Collections.Generic;
using UnityEngine;

public class BarnesHutAlgorithm
{
    public static Particle CalculateGravity(Particle[] particles, float minBorder, float maxBorder)
    {
        if (particles.Length == 0) return null;
        if (particles.Length == 1)
        {
            return particles[0];
        }
        List<Particle> UpRight = new List<Particle>();
        List<Particle> UpLeft = new List<Particle>();
        List<Particle> BottomRight = new List<Particle>();
        List<Particle> BottomLeft = new List<Particle>();

        float Mid = (maxBorder + minBorder) / 2;

        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].Position.y >= Mid)
            {
                if (particles[i].Position.x >= Mid)
                {
                    UpRight.Add(particles[i]);
                }
                else
                {
                    UpLeft.Add(particles[i]);
                }
            }
            else
            {
                if (particles[i].Position.x >= Mid)
                {
                    BottomRight.Add(particles[i]);
                }
                else
                {
                    BottomLeft.Add(particles[i]);
                }
            }
        }
        List<Particle> newParticles = new List<Particle>();
        Particle currentParticle = CalculateGravity(UpRight.ToArray(), Mid, maxBorder);
        if (currentParticle != null)
        {
            newParticles.Add(currentParticle);
        }
        currentParticle = CalculateGravity(UpLeft.ToArray(), Mid, maxBorder);
        if (currentParticle != null)
        {
            newParticles.Add(currentParticle);
        }
        currentParticle = CalculateGravity(BottomRight.ToArray(), minBorder, Mid);
        if (currentParticle != null)
        {
            newParticles.Add(currentParticle);
        }
        currentParticle =CalculateGravity(BottomLeft.ToArray(), minBorder, Mid);
        if (currentParticle != null)
        {
            newParticles.Add(currentParticle);
        }

        Vector2 newPos = new Vector2();
        float newMass = 0;
        int numberOfNotNull = 0;

        for (int i = 0; i < newParticles.Count; i++)
        {
            GravitySimulationHandler.instance.ContinousWallsCollisionDetection(i);
            GravitySimulationHandler.instance.CalculateParticlesVelicities(newParticles.ToArray(), i);
            newPos += newParticles[i].Position;
            newMass += newParticles[i].Mass;
            numberOfNotNull++;
        }

        newPos /= numberOfNotNull;
        return new Particle(newPos, Vector2.zero, newMass);
    }
}