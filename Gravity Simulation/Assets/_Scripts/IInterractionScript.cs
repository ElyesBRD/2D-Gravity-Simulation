using System.Collections.Generic;
using UnityEngine;

public class IInterractionScript : MonoBehaviour
{
    Vector3 ClickedPosition;
    bool isPressingSpawnButton;
    Transform GhostCircle;
    public float spawnWithMass = 1;
    private void Start()
    {
        GhostCircle = Instantiate(GravitySimulationHandler.instance.prefabParticle, Vector3.zero, Quaternion.identity).transform;
        GhostCircle.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isPressingSpawnButton = true;
            ClickedPosition = CursorScript.worldPosition;

            GhostCircle.gameObject.SetActive(true);
            GhostCircle.position = ClickedPosition;
            GhostCircle.GetComponent<SpriteRenderer>().color = Color.green;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            List<Particle> newCirclesArray = new List<Particle>(GravitySimulationHandler.instance.particles);
            newCirclesArray.Add(new Particle(ClickedPosition, CursorScript.worldPosition - ClickedPosition, spawnWithMass));
            GravitySimulationHandler.instance.UpdateParticlesArray(newCirclesArray.ToArray());

            GhostCircle.gameObject.SetActive(false);

            isPressingSpawnButton = false;
        }
    }
    private void OnGUI()
    {
        if (!isPressingSpawnButton) return;
        Debug.DrawLine(CursorScript.worldPosition, ClickedPosition);
    }
}