using UnityEngine;

public class BarnesHutAlgorithm
{
    public static void CreateBarnesHutTree(Particle[] particles, float minBorder, float maxBorder)
    {
        Node tree = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { particles[0], null });
        for (int i = 1; i < particles.Length; i++)
        {
            addNode(tree, particles[i] ,minBorder, maxBorder, 0);
        }
    }
    public static void addNode(Node currentNode,Particle newParticle, float minBorder, float maxBorder, int repetition)
    {
        if (repetition == 2)
        {
            currentNode.particles[1] = newParticle;
        }
        else
        {
            float Mid = (maxBorder + minBorder) / 2;

            if (currentNode.particles[0].Position.y >= Mid)
            {
                if (currentNode.particles[0].Position.x >= Mid)
                {
                    currentNode.upRight = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                }
                else
                {
                    currentNode.upLeft = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                }
            }
            else
            {
                if (currentNode.particles[0].Position.x >= Mid)
                {
                    currentNode.bottomRight = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                }
                else
                {
                    currentNode.bottomLeft = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                }
            }
            currentNode.particles[0] = null;

            if (newParticle.Position.y >= Mid)
            {
                if (newParticle.Position.x >= Mid)
                {
                    if (currentNode.upRight != null)
                    {
                        //currentNode.upRight.particles[1] = newParticle;
                        addNode(currentNode.upRight, newParticle, Mid, maxBorder, repetition + 1);
                    }
                    else
                    {
                        currentNode.upRight = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                    }
                }
                else
                {
                    if (currentNode.upLeft != null)
                    {
                        //currentNode.upLeft.particles[1] = newParticle;
                        addNode(currentNode.upLeft, newParticle, Mid, maxBorder, repetition + 1);
                    }
                    else
                    {
                        currentNode.upLeft = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                    }
                }
            }
            else
            {
                if (newParticle.Position.x >= Mid)
                {
                    if (currentNode.bottomRight != null)
                    {
                        //currentNode.bottomRight.particles[1] = newParticle;
                        addNode(currentNode.bottomRight, newParticle, minBorder, Mid, repetition+1);
                    }
                    else
                    {
                        currentNode.bottomRight = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                    }
                }
                else
                {
                    if (currentNode.bottomLeft != null)
                    {
                        //currentNode.bottomLeft.particles[1] = newParticle;
                        addNode(currentNode.bottomLeft, newParticle, minBorder, Mid, repetition + 1);
                    }
                    else
                    {
                        currentNode.bottomLeft = new Node(Vector2.zero, 0, null, null, null, null, new Particle[2] { currentNode.particles[0], null });
                    }
                }
            }
        }
        if (currentNode.upLeft != null)
        {
            for (int i = 0; i < currentNode.upLeft.particles.Length; i++)
            {
                if (currentNode.upLeft.particles[i] == null) break;
                currentNode.position += currentNode.upLeft.particles[i].Position * currentNode.upLeft.particles[i].Mass;
            }
        }
        if (currentNode.upRight != null)
        {
            for (int i = 0; i < currentNode.upRight.particles.Length; i++)
            {
                if (currentNode.upRight.particles[i] == null) break;
                currentNode.position += currentNode.upRight.particles[i].Position * currentNode.upRight.particles[i].Mass;
            }
        }
        if (currentNode.bottomLeft != null)
        {
            for (int i = 0; i < currentNode.bottomLeft.particles.Length; i++)
            {
                if (currentNode.bottomLeft.particles[i] == null) break;
                currentNode.position += currentNode.bottomLeft.particles[i].Position * currentNode.bottomLeft.particles[i].Mass;
            }
        }
        if (currentNode.bottomRight != null)
        {
            for (int i = 0; i < currentNode.bottomRight.particles.Length; i++)
            {
                if (currentNode.bottomRight.particles[i] == null) break;
                currentNode.position += currentNode.bottomRight.particles[i].Position * currentNode.bottomRight.particles[i].Mass;
            }
        }
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
    public Node (Vector2 position, float mass, Node upLeft, Node upRight, Node bottomLeft, Node bottomRight, Particle[] particles = null)
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