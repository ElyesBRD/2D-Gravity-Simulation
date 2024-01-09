using System.Collections.Generic;
using UnityEngine;

public class BarnesHutAlgorithm
{
    public static Particle CalculateGravity(Particle[] particles, float minBorder, float maxBorder , int repetition)
    {
        if (repetition >= 3)
        {
            Vector2 _newPos = new Vector2();
            float _newMass = 0;
            int _numberOfNotNull = 0;

            for (int i = 0; i < particles.Length; i++)
            {
                if (particles[i] == null) continue;

                GravitySimulationHandler.instance.CalculateParticlesVelicities(particles, i);
                _newPos += particles[i].Position;
                _newMass += particles[i].Mass;
                _numberOfNotNull++;
            }

            _newPos /= _numberOfNotNull;
            return new Particle(_newPos, Vector2.zero, _newMass);
        }
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

        int currentRepetition = 0;
        List<Particle> newParticles = new List<Particle>();
        Particle currentP = null;
        if (UpRight.Count == particles.Length) currentRepetition = repetition+1;
        else currentRepetition = 0;
        currentP = CalculateGravity(UpRight.ToArray(), Mid, maxBorder, currentRepetition);
        if (currentP != null) newParticles.Add(currentP);

        if (UpRight.Count == particles.Length) currentRepetition = repetition + 1;
        else currentRepetition = 0;
        newParticles.Add(CalculateGravity(UpLeft.ToArray(), Mid, maxBorder, currentRepetition));
        if (currentP != null) newParticles.Add(currentP);

        if (UpRight.Count == particles.Length) currentRepetition = repetition + 1;
        else currentRepetition = 0;
        currentP = CalculateGravity(BottomRight.ToArray(), minBorder, Mid, currentRepetition);
        if (currentP != null) newParticles.Add(currentP);

        if (UpRight.Count == particles.Length) currentRepetition = repetition + 1;
        else currentRepetition = 0;
        currentP = CalculateGravity(BottomLeft.ToArray(), minBorder, Mid, currentRepetition);
        if (currentP != null) newParticles.Add(currentP);

        Vector2 newPos = new Vector2();
        float newMass = 0;
        int numberOfNotNull = 0;

        for (int i = 0; i < newParticles.Count; i++)
        {
            GravitySimulationHandler.instance.CalculateParticlesVelicities(newParticles.ToArray(), i);
            newPos += newParticles[i].Position;
            newMass += newParticles[i].Mass;
            numberOfNotNull++;
        }

        newPos /= numberOfNotNull;
        return new Particle(newPos, Vector2.zero, newMass);
    }
}