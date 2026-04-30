using UnityEngine;

public class SnowballRolling : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 180f;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Save forward direction one time only
        moveDirection = transform.forward;
        moveDirection.y = 0f;
        moveDirection.Normalize();
    }

    void FixedUpdate()
    {
        // Move in original saved direction
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);

        // Roll visually
        Vector3 rollAxis = Vector3.Cross(Vector3.up, moveDirection);

        transform.Rotate(
            rollAxis,
            rotationSpeed * Time.fixedDeltaTime,
            Space.World
        );
    }
}