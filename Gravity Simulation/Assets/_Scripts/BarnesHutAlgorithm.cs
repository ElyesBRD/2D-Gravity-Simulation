using System.Collections.Generic;
using UnityEngine;

public class BarnesHutAlgorithm
{
    public static void CreateBarnesHutTree(Particle[] particles, float minBorder, float maxBorder)
    {
        Node node = CalculateGravity(new Node(particles, Vector2.zero, 0, null, null, null, null), minBorder, maxBorder, 0);
    }
    public static Node CalculateGravity(Node currentNode, float minBorder, float maxBorder, int repetition)
    {
        if (currentNode.particles.Length == 1)
        {
            return currentNode;
        }

        List<Particle> UpRight = new List<Particle>();
        List<Particle> UpLeft = new List<Particle>();
        List<Particle> BottomRight = new List<Particle>();
        List<Particle> BottomLeft = new List<Particle>();

        currentNode.widhOfReagon = Mathf.Abs(maxBorder - minBorder);
        float Mid = (maxBorder + minBorder) / 2;

        for (int i = 0; i < currentNode.particles.Length; i++)
        {
            if (currentNode.particles[i].Position.y >= Mid)
            {
                if (currentNode.particles[i].Position.x >= Mid)
                {
                    UpRight.Add(currentNode.particles[i]);
                }
                else
                {
                    UpLeft.Add(currentNode.particles[i]);
                }
            }
            else
            {
                if (currentNode.particles[i].Position.x >= Mid)
                {
                    BottomRight.Add(currentNode.particles[i]);
                }
                else
                {
                    BottomLeft.Add(currentNode.particles[i]);
                }
            }
        }
        return null;
    }
    //public static Particle CalculateGravity(Particle[] particles, float minBorder, float maxBorder, int repetition)
    //{
    //    if (particles.Length == 1)
    //    {
    //        return particles[0];
    //    }
    //    if (repetition == 5)
    //    {
    //        int _amountOfParticlesExist = 0;
    //        Vector2 _medposition = Vector2.zero;
    //        float _totalMass = 0;
    //        for (int i = 0; i < particles.Length; i++)
    //        {
    //            if (particles[i] != null)
    //            {
    //                _amountOfParticlesExist++;
    //                _medposition += particles[i].Position;
    //                _totalMass += particles[i].Mass;
    //            }
    //        }

    //        return new Particle(_medposition / _amountOfParticlesExist, Vector2.zero, _totalMass);
    //    }
    //    List<Particle> UpRight = new List<Particle>();
    //    List<Particle> UpLeft = new List<Particle>();
    //    List<Particle> BottomRight = new List<Particle>();
    //    List<Particle> BottomLeft = new List<Particle>();

    //    float Mid = (maxBorder + minBorder) / 2;

    //    for (int i = 0; i < particles.Length; i++)
    //    {
    //        if (particles[i].Position.y >= Mid)
    //        {
    //            if (particles[i].Position.x >= Mid)
    //            {
    //                UpRight.Add(particles[i]);
    //            }
    //            else
    //            {
    //                UpLeft.Add(particles[i]);
    //            }
    //        }
    //        else
    //        {
    //            if (particles[i].Position.x >= Mid)
    //            {
    //                BottomRight.Add(particles[i]);
    //            }
    //            else
    //            {
    //                BottomLeft.Add(particles[i]);
    //            }
    //        }
    //    }
    //    int currentRepetition = 0;
    //    if (UpLeft.Count == particles.Length || UpRight.Count == particles.Length || BottomRight.Count == particles.Length || BottomLeft.Count == particles.Length)
    //    {
    //        currentRepetition += repetition + 1;
    //    }

    //    List<Particle> sommeOfParticles = new List<Particle>();
    //    if (UpLeft.Count > 0) sommeOfParticles.Add(CalculateGravity(UpLeft.ToArray(), Mid, maxBorder, currentRepetition));
    //    if (UpRight.Count > 0) sommeOfParticles.Add(CalculateGravity(UpRight.ToArray(), Mid, maxBorder, currentRepetition));
    //    if (BottomLeft.Count > 0) sommeOfParticles.Add(CalculateGravity(BottomLeft.ToArray(), minBorder, Mid, currentRepetition));
    //    if (BottomRight.Count > 0) sommeOfParticles.Add(CalculateGravity(BottomRight.ToArray(), minBorder, Mid, currentRepetition));

    //    Vector2 medposition = Vector2.zero;
    //    float totalMass = 0;
    //    for (int i = 0; i < sommeOfParticles.Count; i++)
    //    {
    //        medposition += sommeOfParticles[i].Position;
    //        totalMass += sommeOfParticles[i].Mass;
    //        GravitySimulationHandler.instance.CalculateParticlesVelicities(sommeOfParticles.ToArray(), i);
    //    }
    //    return new Particle(medposition / sommeOfParticles.Count, Vector2.zero, totalMass);
    //}
}
public class Node
{
    public Particle[] particles;
    public Vector2 position;
    public float mass;
    public float widhOfReagon;

    public Node upLeft;
    public Node upRight;
    public Node bottomLeft;
    public Node bottomRight;
    public Node (Particle[] particles, Vector2 position, float mass, Node upLeft, Node upRight, Node bottomLeft, Node bottomRight)
    {
        this.particles = particles;
        this.position = position;
        this.mass = mass;
        this.upLeft = upLeft;
        this.upRight = upRight;
        this.bottomLeft = bottomLeft;
        this.bottomRight = bottomRight;
    }
}