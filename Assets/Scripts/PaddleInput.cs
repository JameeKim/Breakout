using UnityEngine;

public class PaddleInput : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public BoxCollider2D wallLeft;
    public BoxCollider2D wallRight;

    private Paddle paddle;
    private new Rigidbody2D rigidbody;
    private float leftBoundary;
    private float rightBoundary;

    private void Awake()
    {
        paddle = GetComponent<Paddle>();
        rigidbody = GetComponent<Rigidbody2D>();

        leftBoundary = wallLeft.transform.position.x + (wallLeft.size.x / 2.0f);
        rightBoundary = wallRight.transform.position.x - (wallRight.size.x / 2.0f);
    }

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(movement * moveSpeed, 0.0f);
        float snapThreshold = Mathf.Abs(movement * moveSpeed * Time.fixedDeltaTime);

        if (movement < 0.0f)
        {
            float leftmostPosition = leftBoundary + (paddle.width / 2.0f);
            if (rigidbody.position.x <= leftmostPosition + snapThreshold)
            {
                rigidbody.velocity = new Vector2();
                rigidbody.position = new Vector2(
                    leftmostPosition,
                    rigidbody.position.y);
            }
        }
        else if (movement > 0.0f)
        {
            float rightmostPosition = rightBoundary - (paddle.width / 2.0f);
            if (rigidbody.position.x >= rightmostPosition - snapThreshold)
            {
                rigidbody.velocity = new Vector2();
                rigidbody.position = new Vector2(
                    rightmostPosition,
                    rigidbody.position.y);
            }
        }

        if (Input.GetButtonDown("Action"))
        {
            paddle.ShootBall();
        }
    }
}
