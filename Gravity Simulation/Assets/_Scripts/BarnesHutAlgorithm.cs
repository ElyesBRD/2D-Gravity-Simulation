using System.Collections.Generic;
using UnityEngine;

public class BarnesHutAlgorithm
{
    public static Particle CalculateGravity(Particle[] particles, float minBorder, float maxBorder, int repetition)
    {
        if (particles.Length == 0) return null;
        if (particles.Length == 1)
        {
            return particles[0];
        }
        if (repetition == 4)
        {
            int _amountOfParticlesExist = 0;
            Vector2 _medposition = Vector2.zero;
            float _totalMass = 0;
            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] != null)
                {
                    _amountOfParticlesExist++;
                    _medposition += particles[i].Position;
                    _totalMass += particles[i].Mass;
                }
            }

            return new Particle(_medposition / _amountOfParticlesExist, Vector2.zero, _totalMass);
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
        int currentRepetition = 0;
        if (UpLeft.Count == particles.Length || UpRight.Count == particles.Length || BottomRight.Count == particles.Length || BottomLeft.Count == particles.Length)
        {
            currentRepetition += repetition + 1;
        }

        List<Particle> sommeOfParticles = new List<Particle>();
        sommeOfParticles.Add(CalculateGravity(UpLeft.ToArray(), Mid, maxBorder, currentRepetition));
        sommeOfParticles.Add(CalculateGravity(UpRight.ToArray(), Mid, maxBorder, currentRepetition));
        sommeOfParticles.Add(CalculateGravity(BottomLeft.ToArray(), minBorder, Mid, currentRepetition));
        sommeOfParticles.Add(CalculateGravity(BottomRight.ToArray(), minBorder, Mid, currentRepetition));

        int amountOfParticlesExist = 0;
        Vector2 medposition = Vector2.zero;
        float totalMass = 0;
        int index = 0;
        while (index != sommeOfParticles.Count)
        {
            if (sommeOfParticles[index] != null)
            {
                amountOfParticlesExist++;
                medposition += sommeOfParticles[index].Position;
                totalMass += sommeOfParticles[index].Mass;
                index++;
            }
            else
            {
                sommeOfParticles.RemoveAt(index);
            }
        }
        for (int i = 0; i < sommeOfParticles.Count; i++)
        {
            GravitySimulationHandler.instance.CalculateParticlesVelicities(sommeOfParticles.ToArray(), i);
        }
        return new Particle(medposition / amountOfParticlesExist, Vector2.zero, totalMass);
    }
}