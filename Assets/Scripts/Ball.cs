using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float speedIncrement = 0.2f;
    public float angleRangeMin = 0.1f;
    public float angleRangeMax = 0.3f;

    private Rigidbody2D rigidBody;
    private float currentSpeed = 10.0f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rigidBody.velocity;
        rigidBody.velocity = currentSpeed * velocity.normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            currentSpeed += speedIncrement;
        }
    }

    private void Shoot()
    {
        // The shoot angle is not done by a single random range to avoid shooting straight
        // vertically

        float directionMultiplier = 1.0f;
        if (Random.Range(-1.0f, 1.0f) < 0.0f)
        {
            directionMultiplier = -1.0f;
        }

        float angle = Random.Range(angleRangeMin, angleRangeMax) * directionMultiplier;
        Vector2 direction = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
        rigidBody.velocity = moveSpeed * direction;
    }
}
