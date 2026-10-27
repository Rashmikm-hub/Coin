using UnityEngine;

public class MovingPlatformMovement : MonoBehaviour
{
    public float moveDistance = 3f;
    public float moveSpeed = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(
            Time.time * moveSpeed,
            moveDistance
        ) - moveDistance / 2f;

        transform.position = startPosition + Vector3.right * movement;
    }
}