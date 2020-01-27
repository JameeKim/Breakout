using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PaddleInput : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public BoxCollider2D wallLeft;
    public BoxCollider2D wallRight;

    private Paddle paddle;
    private Rigidbody2D rigidBody;
    private float leftBoundary;
    private float rightBoundary;

    private void Awake()
    {
        paddle = GetComponent<Paddle>();
        rigidBody = GetComponent<Rigidbody2D>();

        leftBoundary = wallLeft.transform.position.x + (wallLeft.size.x / 2.0f);
        rightBoundary = wallRight.transform.position.x - (wallRight.size.x / 2.0f);
    }

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(movement * moveSpeed, 0.0f);
        float snapThreshold = Mathf.Abs(movement * moveSpeed * Time.fixedDeltaTime);

        if (movement < 0.0f)
        {
            float leftmostPosition = leftBoundary + (paddle.width / 2.0f);
            if (rigidBody.position.x <= leftmostPosition + snapThreshold)
            {
                rigidBody.velocity = new Vector2();
                rigidBody.position = new Vector2(leftmostPosition, rigidBody.position.y);
            }
        }
        else if (movement > 0.0f)
        {
            float rightmostPosition = rightBoundary - (paddle.width / 2.0f);
            if (rigidBody.position.x >= rightmostPosition - snapThreshold)
            {
                rigidBody.velocity = new Vector2();
                rigidBody.position = new Vector2(rightmostPosition, rigidBody.position.y);
            }
        }

        if (Input.GetButtonDown("Action"))
        {
            paddle.ShootBall();
        }
    }
}
