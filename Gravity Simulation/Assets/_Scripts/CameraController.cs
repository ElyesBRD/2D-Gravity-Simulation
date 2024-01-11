using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 direction;
    float scrollWheelDirection;
    public float Movementspeed;
    public float ZoomSpeed;
    private void Update()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");
        direction = new Vector3(xAxis, yAxis, 0);
        scrollWheelDirection = -1 * Input.mouseScrollDelta.y * ZoomSpeed;
        Zoom();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        //transform.position = transform.position + direction;
        transform.Translate(direction * Movementspeed, Space.World);
    }
    void Zoom()
    {
        Camera.main.orthographicSize += scrollWheelDirection;
    }
}