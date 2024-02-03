using UnityEngine;

public class BarnesHutAlgorithm
{
    public static void CreateBarnesHutTree(Particle[] particles, float minBorder, float maxBorder)
    {
        Node tree = new Node(null, null, null, null, null);
        tree.dWidhOfBox = Mathf.Abs(maxBorder - minBorder);
        for (int i = 0; i < particles.Length; i++)
        {
            addNode(tree, particles[i], minBorder, maxBorder, minBorder, maxBorder, 0);
        }
    }
    public static void addNode(Node currentNode, Particle newParticle, float xMinBorder, float xMaxBorder, float yMinBorder, float yMaxBorder, int repetition)
    {
        float xMid = (xMaxBorder + xMinBorder) / 2;
        float yMid = (yMaxBorder + yMinBorder) / 2;
        float dWidhOfBox = Mathf.Abs(xMaxBorder - xMinBorder);
        float nextdWidhOfBox = dWidhOfBox / 2;
        float totalMass = 0;
        Vector2 centerOfMass = Vector2.zero;
        if (repetition == 4)
        {
            totalMass += newParticle.Mass + currentNode.NodeParticle.Mass;
            centerOfMass += newParticle.Position + currentNode.NodeParticle.Position;
        }
        else
        {
            if (currentNode.particle != null)
            {
                if (currentNode.particle.Position.y >= yMid)
                {
                    if (currentNode.particle.Position.x >= xMid)
                    {
                        currentNode.upRight = new Node(null, null, null, null, currentNode.particle);
                        currentNode.upRight.SetNodeParticle();
                    }
                    else
                    {
                        currentNode.upLeft = new Node(null, null, null, null, currentNode.particle);
                        currentNode.upLeft.SetNodeParticle();
                    }
                }
                else
                {
                    if (currentNode.particle.Position.x >= xMid)
                    {
                        currentNode.bottomRight = new Node(null, null, null, null, currentNode.particle);
                        currentNode.bottomRight.SetNodeParticle();
                    }
                    else
                    {
                        currentNode.bottomLeft = new Node(null, null, null, null, currentNode.particle);
                        currentNode.bottomLeft.SetNodeParticle();
                    }
                }
                currentNode.particle = null;
            }
            if (newParticle.Position.y >= yMid)
            {
                if (newParticle.Position.x >= xMid)
                {
                    if (currentNode.upRight != null)
                    {
                        addNode(currentNode.upRight, newParticle, xMid, xMaxBorder, yMid, yMaxBorder, repetition + 1);
                    }
                    else
                    {
                        currentNode.upRight = new Node(null, null, null, null, newParticle);
                        currentNode.upRight.dWidhOfBox = nextdWidhOfBox;
                        currentNode.upRight.SetNodeParticle();
                    }
                }
                else
                {
                    if (currentNode.upLeft != null)
                    {
                        addNode(currentNode.upLeft, newParticle, xMinBorder, xMid, yMid, yMaxBorder, repetition + 1);
                    }
                    else
                    {
                        currentNode.upLeft = new Node(null, null, null, null, newParticle);
                        currentNode.upLeft.dWidhOfBox = nextdWidhOfBox;
                        currentNode.upLeft.SetNodeParticle();
                    }
                }
            }
            else
            {
                if (newParticle.Position.x >= xMid)
                {
                    if (currentNode.bottomRight != null)
                    {
                        addNode(currentNode.bottomRight, newParticle ,xMid, xMaxBorder, yMinBorder, yMid, repetition + 1);
                    }
                    else
                    {
                        currentNode.bottomRight = new Node(null, null, null, null, newParticle);
                        currentNode.bottomRight.dWidhOfBox = nextdWidhOfBox;
                        currentNode.bottomRight.SetNodeParticle();
                    }
                }
                else
                {
                    if (currentNode.bottomLeft != null)
                    {
                        addNode(currentNode.bottomLeft, newParticle, xMinBorder, xMid, yMinBorder, yMid, repetition + 1);
                    }
                    else
                    {
                        currentNode.bottomLeft = new Node(null, null, null, null, newParticle);
                        currentNode.bottomLeft.dWidhOfBox = nextdWidhOfBox;
                        currentNode.bottomLeft.SetNodeParticle();
                    }
                }
            }
        }
        //center of mass

        totalMass += currentNode.upRight != null ? currentNode.upRight.NodeParticle.Mass : 0;
        centerOfMass += currentNode.upRight != null ? currentNode.upRight.NodeParticle.Position : Vector2.zero;

        totalMass += currentNode.upLeft != null ? currentNode.upLeft.NodeParticle.Mass : 0;
        centerOfMass += currentNode.upLeft != null ? currentNode.upLeft.NodeParticle.Position : Vector2.zero;

        totalMass += currentNode.bottomRight != null ? currentNode.bottomRight.NodeParticle.Mass : 0;
        centerOfMass += currentNode.bottomRight != null ? currentNode.bottomRight.NodeParticle.Position : Vector2.zero;

        totalMass += currentNode.bottomLeft != null ? currentNode.bottomLeft.NodeParticle.Mass : 0;
        centerOfMass += currentNode.bottomLeft != null ? currentNode.bottomLeft.NodeParticle.Position : Vector2.zero;

        if (totalMass != 0)
        {
            currentNode.NodeParticle.Mass = totalMass;
            currentNode.NodeParticle.Position = centerOfMass / totalMass;
        }
    }
}
public class Node
{
    public Particle NodeParticle;
    public Particle particle;
    public float dWidhOfBox;

    public Node upLeft;
    public Node upRight;
    public Node bottomLeft;
    public Node bottomRight;
    public Node (Node upLeft, Node upRight, Node bottomLeft, Node bottomRight, Particle particle = null)
    {
        this.particle = particle;
        this.upLeft = upLeft;
        this.upRight = upRight;
        this.bottomLeft = bottomLeft;
        this.bottomRight = bottomRight;
        NodeParticle = new Particle(Vector2.zero, Vector2.zero, 0);
    }
    public void SetNodeParticle()
    {
        NodeParticle.Mass = particle.Mass; NodeParticle.Position = particle.Position;
    }
}
#region old aproach
//using UnityEngine;

//public class BarnesHutAlgorithm
//{
//    public static void CreateBarnesHutTree(Particle[] particles, float minBorder, float maxBorder)
//    {
//        Node tree = new Node(Vector2.zero, null, null, null, null, new Particle { null, null });
//        for (int i = 0; i < particles.Length; i++)
//        {
//            addNode(tree, particles[i], minBorder, maxBorder, minBorder, maxBorder, 0);
//        }
//    }
//    public static void addNode(Node currentNode, Particle newParticle, float xMinBorder, float xMaxBorder, float yMinBorder, float yMaxBorder, int repetition)
//    {
//        float xMid = (xMaxBorder + xMinBorder) / 2;
//        float yMid = (yMaxBorder + yMinBorder) / 2;
//        if (repetition == 4)
//        {
//            currentNode.particles[1] = newParticle;
//        }
//        else
//        {
//            if (currentNode.particle != null)
//            {
//                if (currentNode.particle.Position.y >= yMid)
//                {
//                    if (currentNode.particle.Position.x >= xMid)
//                    {
//                        currentNode.upRight = new Node(Vector2.zero, null, null, null, null, new Particle { currentNode.particle, null });
//                    }
//                    else
//                    {
//                        currentNode.upLeft = new Node(Vector2.zero, null, null, null, null, new Particle { currentNode.particle, null });
//                    }
//                }
//                else
//                {
//                    if (currentNode.particle.Position.x >= xMid)
//                    {
//                        currentNode.bottomRight = new Node(Vector2.zero, null, null, null, null, new Particle { currentNode.particle, null });
//                    }
//                    else
//                    {
//                        currentNode.bottomLeft = new Node(Vector2.zero, null, null, null, null, new Particle { currentNode.particle, null });
//                    }
//                }
//                currentNode.particle = null;
//            }

//            if (newParticle.Position.y >= yMid)
//            {
//                if (newParticle.Position.x >= xMid)
//                {
//                    if (currentNode.upRight != null)
//                    {
//                        //currentNode.upRight.particles[1] = newParticle;
//                        addNode(currentNode.upRight, newParticle, xMid, xMaxBorder, yMid, yMaxBorder, repetition + 1);
//                    }
//                    else
//                    {
//                        currentNode.upRight = new Node(Vector2.zero, null, null, null, null, new Particle { newParticle, null });
//                    }
//                }
//                else
//                {
//                    if (currentNode.upLeft != null)
//                    {
//                        //currentNode.upLeft.particles[1] = newParticle;
//                        addNode(currentNode.upLeft, newParticle, xMinBorder, xMid, yMid, yMaxBorder, repetition + 1);
//                    }
//                    else
//                    {
//                        currentNode.upLeft = new Node(Vector2.zero, null, null, null, null, new Particle { newParticle, null });
//                    }
//                }
//            }
//            else
//            {
//                if (newParticle.Position.x >= xMid)
//                {
//                    if (currentNode.bottomRight != null)
//                    {
//                        //currentNode.bottomRight.particles[1] = newParticle;
//                        addNode(currentNode.bottomRight, newParticle, xMid, xMaxBorder, yMinBorder, yMid, repetition + 1);
//                    }
//                    else
//                    {
//                        currentNode.bottomRight = new Node(Vector2.zero, null, null, null, null, new Particle { newParticle, null });
//                    }
//                }
//                else
//                {
//                    if (currentNode.bottomLeft != null)
//                    {
//                        //currentNode.bottomLeft.particles[1] = newParticle;
//                        addNode(currentNode.bottomLeft, newParticle, xMinBorder, xMid, yMinBorder, yMid, repetition + 1);
//                    }
//                    else
//                    {
//                        currentNode.bottomLeft = new Node(Vector2.zero, null, null, null, null, new Particle { newParticle, null });
//                    }
//                }
//            }
//        }
//        //center of mass
//        float totalMass = 0;
//        Vector2 centerOfMass = Vector2.zero;

//        totalMass += currentNode.upRight != null ? calculateTotalMass(currentNode.upRight.particles) : 0;
//        centerOfMass += currentNode.upRight != null ? calculateCenterOfMass(currentNode.upRight.particles) : Vector2.zero;

//        totalMass += currentNode.upLeft != null ? calculateTotalMass(currentNode.upLeft.particles) : 0;
//        centerOfMass += currentNode.upLeft != null ? calculateCenterOfMass(currentNode.upLeft.particles) : Vector2.zero;

//        totalMass += currentNode.bottomRight != null ? calculateTotalMass(currentNode.bottomRight.particles) : 0;
//        centerOfMass += currentNode.bottomRight != null ? calculateCenterOfMass(currentNode.bottomRight.particles) : Vector2.zero;

//        totalMass += currentNode.bottomLeft != null ? calculateTotalMass(currentNode.bottomLeft.particles) : 0;
//        centerOfMass += currentNode.bottomLeft != null ? calculateCenterOfMass(currentNode.bottomLeft.particles) : Vector2.zero;

//        if (totalMass != 0)
//        {
//            currentNode.MainParticle.Mass = totalMass;
//            currentNode.MainParticle.Position = centerOfMass / totalMass;
//        }
//    }
//    public static float calculateTotalMass(Particle[] particles)
//    {
//        float totalMass = 0;
//        for (int i = 0; i < particles.Length; i++)
//        {
//            totalMass += particles[i] != null ? particles[i].Mass : 0;
//        }
//        return totalMass;
//    }
//    public static Vector2 calculateCenterOfMass(Particle[] particles)
//    {
//        Vector2 centerOfMass = Vector2.zero;
//        for (int i = 0; i < particles.Length; i++)
//        {
//            centerOfMass += particles[i] != null ? particles[i].Position * particles[i].Mass : Vector2.zero;
//        }
//        return centerOfMass;
//    }
//    //public static Particle CalculateGravity(Particle[] particles, float minBorder, float maxBorder, int repetition)
//    //{
//    //    if (particles.Length == 1)
//    //    {
//    //        return particle;
//    //    }
//    //    if (repetition == 5)
//    //    {
//    //        int _amountOfParticlesExist = 0;
//    //        Vector2 _medposition = Vector2.zero;
//    //        float _totalMass = 0;
//    //        for (int i = 0; i < particles.Length; i++)
//    //        {
//    //            if (particles[i] != null)
//    //            {
//    //                _amountOfParticlesExist++;
//    //                _medposition += particles[i].Position;
//    //                _totalMass += particles[i].Mass;
//    //            }
//    //        }

//    //        return new Particle(_medposition / _amountOfParticlesExist, Vector2.zero, _totalMass);
//    //    }
//    //    List<Particle> UpRight = new List<Particle>();
//    //    List<Particle> UpLeft = new List<Particle>();
//    //    List<Particle> BottomRight = new List<Particle>();
//    //    List<Particle> BottomLeft = new List<Particle>();

//    //    float Mid = (maxBorder + minBorder) / 2;

//    //    for (int i = 0; i < particles.Length; i++)
//    //    {
//    //        if (particles[i].Position.y >= Mid)
//    //        {
//    //            if (particles[i].Position.x >= Mid)
//    //            {
//    //                UpRight.Add(particles[i]);
//    //            }
//    //            else
//    //            {
//    //                UpLeft.Add(particles[i]);
//    //            }
//    //        }
//    //        else
//    //        {
//    //            if (particles[i].Position.x >= Mid)
//    //            {
//    //                BottomRight.Add(particles[i]);
//    //            }
//    //            else
//    //            {
//    //                BottomLeft.Add(particles[i]);
//    //            }
//    //        }
//    //    }
//    //    int currentRepetition = 0;
//    //    if (UpLeft.Count == particles.Length || UpRight.Count == particles.Length || BottomRight.Count == particles.Length || BottomLeft.Count == particles.Length)
//    //    {
//    //        currentRepetition += repetition + 1;
//    //    }

//    //    List<Particle> sommeOfParticles = new List<Particle>();
//    //    if (UpLeft.Count > 0) sommeOfParticles.Add(CalculateGravity(UpLeft.ToArray(), Mid, maxBorder, currentRepetition));
//    //    if (UpRight.Count > 0) sommeOfParticles.Add(CalculateGravity(UpRight.ToArray(), Mid, maxBorder, currentRepetition));
//    //    if (BottomLeft.Count > 0) sommeOfParticles.Add(CalculateGravity(BottomLeft.ToArray(), minBorder, Mid, currentRepetition));
//    //    if (BottomRight.Count > 0) sommeOfParticles.Add(CalculateGravity(BottomRight.ToArray(), minBorder, Mid, currentRepetition));

//    //    Vector2 medposition = Vector2.zero;
//    //    float totalMass = 0;
//    //    for (int i = 0; i < sommeOfParticles.Count; i++)
//    //    {
//    //        medposition += sommeOfParticles[i].Position;
//    //        totalMass += sommeOfParticles[i].Mass;
//    //        GravitySimulationHandler.instance.CalculateParticlesVelicities(sommeOfParticles.ToArray(), i);
//    //    }
//    //    return new Particle(medposition / sommeOfParticles.Count, Vector2.zero, totalMass);
//    //}
//}
//public class Node
//{
//    public Particle[] particles;
//    public Particle MainParticle;
//    public Vector2 position;
//    public float widhOfReagon;

//    public Node upLeft;
//    public Node upRight;
//    public Node bottomLeft;
//    public Node bottomRight;
//    public Node(Vector2 position, Node upLeft, Node upRight, Node bottomLeft, Node bottomRight, Particle[] particles = null)
//    {
//        this.particles = particles;
//        this.position = position;
//        this.upLeft = upLeft;
//        this.upRight = upRight;
//        this.bottomLeft = bottomLeft;
//        this.bottomRight = bottomRight;
//        MainParticle = new Particle(Vector2.zero, Vector2.zero, 0);
//    }
//}
#endregion