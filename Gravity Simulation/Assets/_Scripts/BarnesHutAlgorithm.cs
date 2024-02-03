using UnityEngine;
public class BarnesHutAlgorithm
{
    public static void CreateBarnesHutTree(Particle[] particles, float minBorder, float maxBorder)
    {
        Node tree = new Node();
        tree.dWidthOfBox = Mathf.Abs(maxBorder - minBorder);
        for (int i = 0; i < particles.Length; i++)
        {
            addNode(tree, particles[i], minBorder, maxBorder, minBorder, maxBorder);
        }
    }
    public static void addNode(Node currentNode, Particle newParticle, float xMinBorder, float xMaxBorder, float yMinBorder, float yMaxBorder)
    {
        float totalMass = 0;
        Vector2 centerOfMass = Vector2.zero;

        if (currentNode.particle != null && (currentNode.particle.Position - newParticle.Position).magnitude < .5f)
        {
            totalMass += newParticle.Mass + currentNode.NodeParticle.Mass;
            centerOfMass += newParticle.Position + currentNode.NodeParticle.Position;
        }
        else
        {
            float xMid = (xMaxBorder + xMinBorder) / 2;
            float yMid = (yMaxBorder + yMinBorder) / 2;
            float dWidthOfBox = Mathf.Abs(xMaxBorder - xMinBorder);
            float nextdWidthOfBox = dWidthOfBox / 2;

            if (currentNode.particle != null)
            {
                SortParticleInChildrenNodes(currentNode, currentNode.particle, xMid, xMinBorder, xMaxBorder, yMid, yMinBorder, yMaxBorder, nextdWidthOfBox);
                currentNode.particle = null;
            }
            SortParticleInChildrenNodes(currentNode, newParticle, xMid, xMinBorder, xMaxBorder, yMid, yMinBorder, yMaxBorder, nextdWidthOfBox);
        }

        CalculateNodeParticleCenterOfMass(currentNode, totalMass, centerOfMass);
    }
    public static void SortParticleInChildrenNodes(Node currentNode, Particle particle, float xMid, float xMinBorder, float xMaxBorder, float yMid, float yMinBorder, float yMaxBorder, float nextdWidthOfBox)
    {
        if (particle.Position.y >= yMid)
        {
            if (particle.Position.x >= xMid)
            {
                currentNode.upRight = addParticleToChildNode(currentNode.upRight, particle, xMid, xMaxBorder, yMid, yMaxBorder, nextdWidthOfBox);
            }
            else
            {
                currentNode.upLeft = addParticleToChildNode(currentNode.upLeft, particle, xMinBorder, xMid, yMid, yMaxBorder, nextdWidthOfBox);
            }
        }
        else
        {
            if (particle.Position.x >= xMid)
            {
                currentNode.bottomRight = addParticleToChildNode(currentNode.bottomRight, particle, xMid, xMaxBorder, yMinBorder, yMid, nextdWidthOfBox);
            }
            else
            {
                currentNode.bottomLeft = addParticleToChildNode(currentNode.bottomLeft, particle, xMinBorder, xMid, yMinBorder, yMid, nextdWidthOfBox);
            }
        }
    }
    public static Node addParticleToChildNode(Node child, Particle particle, float xMinBorder, float xMaxBorder, float yMinBorder, float yMaxBorder, float nextdWidthOfBox)
    {
        if (child != null)
        {
            addNode(child, particle, xMinBorder, xMaxBorder, yMinBorder, yMaxBorder);
            return child;
        }
        else
        {
            Node newChild = new Node(particle);
            newChild.dWidthOfBox = nextdWidthOfBox;
            newChild.InitializeNodeParticle();
            return newChild;
        }
    }
    public static void CalculateNodeParticleCenterOfMass(Node currentNode, float totalMass, Vector2 centerOfMass)
    {
        UpdateCenterOfMassAndTotalMass(currentNode.upRight, ref totalMass, ref centerOfMass);
        UpdateCenterOfMassAndTotalMass(currentNode.upLeft, ref totalMass, ref centerOfMass);
        UpdateCenterOfMassAndTotalMass(currentNode.bottomRight, ref totalMass, ref centerOfMass);
        UpdateCenterOfMassAndTotalMass(currentNode.bottomLeft, ref totalMass, ref centerOfMass);

        if (totalMass != 0)
        {
            currentNode.NodeParticle.Mass = totalMass;
            currentNode.NodeParticle.Position = centerOfMass / totalMass;
        }
    }

    public static void UpdateCenterOfMassAndTotalMass(Node childNode, ref float totalMass, ref Vector2 centerOfMass)
    {
        if (childNode != null)
        {
            totalMass += childNode.NodeParticle.Mass;
            centerOfMass += childNode.NodeParticle.Mass * childNode.NodeParticle.Position;
        }
    }
}
public class Node
{
    public Particle NodeParticle;
    public Particle particle;
    public float dWidthOfBox;

    public Node upLeft;
    public Node upRight;
    public Node bottomLeft;
    public Node bottomRight;
    public Node(Particle particle = null)
    {
        this.particle = particle;
        NodeParticle = new Particle(Vector2.zero, Vector2.zero, 0);
    }
    public void InitializeNodeParticle()
    {
        NodeParticle.Mass = particle.Mass;
        NodeParticle.Position = particle.Position;
    }
}