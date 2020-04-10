using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10.0f;
    public float speedIncrement = 0.2f;

    [Header("Shooting")]
    public bool autoShoot = true;
    public float angleRangeMin = 0.1f;
    public float angleRangeMax = 0.3f;

    private Rigidbody2D rigidBody;

    public float CurrentSpeed { get; set; }

    public int BallId { get; set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        CurrentSpeed = moveSpeed;
    }

    private void Start()
    {
        GameController.Instance.BallManager.AddBall(this);

        if (autoShoot)
            Shoot();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rigidBody.velocity;
        rigidBody.velocity = CurrentSpeed * velocity.normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            CurrentSpeed += speedIncrement;
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
