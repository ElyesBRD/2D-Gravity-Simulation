using UnityEngine;
public class BarnesHutDepthFirstSearch
{
    public float Theta = .5f;
    public float Gravitational_constant = 1;
    public void SimulateGravity(Node root, Particle[] particles, float Theta, float Gravitational_constant)
    {
        this.Theta = Theta;
        this.Gravitational_constant = Gravitational_constant;
        for (int i = 0; i < particles.Length; i++)
        {
            traversesBarnesHutTree(root, particles[i]);
        }
    }
    public void traversesBarnesHutTree(Node currentNode, Particle particle)
    {
        if (currentNode == null) return;
        float distance = Vector2.Distance(particle.Position, currentNode.NodeParticle.Position);
        if (distance / currentNode.dWidthOfBox < Theta)
        {
            CalculateParticleVelicitie(particle, currentNode.NodeParticle, distance);
        }
        else
        {
            traversesBarnesHutTree(currentNode.upRight, particle);
            traversesBarnesHutTree(currentNode.upLeft, particle);
            traversesBarnesHutTree(currentNode.bottomRight, particle);
            traversesBarnesHutTree(currentNode.bottomLeft, particle);
        }
    }
    public void CalculateParticleVelicitie(Particle currentParticles, Particle NodeParticle, float distance)
    {
        if (distance < .2f) return;

        Vector2 direction = NodeParticle.Position - currentParticles.Position;

        float force = (Gravitational_constant * currentParticles.Mass * NodeParticle.Mass) / (distance * distance);
        Vector2 forceVector = force * direction.normalized;

        // Apply force to particles based on their masses
        currentParticles.Acceleration += Vector2.one * forceVector / currentParticles.Mass;
    }
}