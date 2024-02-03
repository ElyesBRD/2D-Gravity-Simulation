# 2D Gravity Simulation
A unity project that simulates 2D particles moving around and pulling each other using neuton's third law of motion.
For Optimization im using Barnes-hut Algorithm, and depth first search to traverse the quad tree.

# Time Complexity
Building the tree is O(n log n)
Traversing the quad tree O(n log n)

# How much can it handle
10K particles with a low end pc gives about 5fps, still didnt test it with a powerful pc.
It depends in the amount of spread/Border size of the particles and box bounderies.

# How To Use
After opening the project in unity (Unity 2021.3.16f1 version was created with):
* Select "SimulationHandler" in the Hierarchy.
* in the inspector you gonna find a script called "GravitySimulationHandler".
* there you can Control all the variables you need for the simulation.
* Start the Scene.

# How To Open Build Mode
* Open the Repo Folder
* Open File Called: "Gravity Simulation"
* Open File Called: "Build"
* Open: "Gravity Simulation.exe"
* (to close the simulation window: press Alt + F4, or Alt + Enter and then Close window button)

# Note
The prject is finished, but it just needs some UI so you can play it in build mode freely.
the fps will drop when the border box size increases and the amound of particles increases.
