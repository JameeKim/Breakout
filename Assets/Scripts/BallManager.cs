using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallManager : MonoBehaviour
{
    [Min(10.0f)]
    public float splitAngleMin = 30.0f;

    [Min(30.0f)]
    public float splitAngleMax = 90.0f;

    public GameObject ballPrefab;

    public AudioClip splitSound;

    public UnityEvent onGameOver;

    private readonly List<Ball> balls = new List<Ball>();

    public void AddBall(Ball newBall)
    {
        balls.Add(newBall);
        newBall.BallId = balls.Count - 1;
    }

    public void RemoveBall(Ball ball)
    {
        int ballId = ball.BallId;

        if (ballId >= balls.Count)
        {
            Debug.LogError($"There are only {balls.Count} balls but ball with id {ballId} requested remove");
            return;
        }

        if (balls.Count == 1)
        {
            onGameOver.Invoke();
            return;
        }

        balls.RemoveAt(ballId);
        for (int i = 0; i < balls.Count; i++)
            balls[i].BallId = i;
    }

    public void Split()
    {
        int index = Random.Range(0, balls.Count);
        Ball ball = balls[index];

        Rigidbody2D rigidBody = ball.GetComponent<Rigidbody2D>();
        Vector2 position = rigidBody.position;
        Vector2 velocity = rigidBody.velocity;
        Vector2 direction = velocity.normalized; // e^(i * a) = cos(a) + i * sin(a)

        float angleDiff = Random.Range(splitAngleMin, splitAngleMax) * Mathf.Deg2Rad;
        float leftAngle = Random.Range(angleDiff / 4, angleDiff * 3 / 4);
        Vector2 rotation = new Vector2(Mathf.Cos(leftAngle), Mathf.Sin(leftAngle)); // e^(i * b) = cos(b) + i * sin(b)

        Vector2 leftDirection = new Vector2( // e^(i * (a+b)) -> rotate counterclockwise
            direction.x * rotation.x - direction.y * rotation.y,  // cos(a+b) = cos(a)cos(b) - sin(a)sin(b)
            direction.y * rotation.x + direction.x * rotation.y); // sin(a+b) = sin(a)cos(b) + cos(a)sin(b)
        Vector2 rightDirection = new Vector2( // e^(i * (a-b)) -> rotate clockwise
            direction.x * rotation.x + direction.y * rotation.y,  // cos(a-b) = cos(a)cos(b) + sin(a)sin(b)
            direction.y * rotation.x - direction.x * rotation.y); // sin(a-b) = sin(a)cos(b) - cos(a)sin(b)

        rigidBody.velocity = leftDirection * ball.CurrentSpeed;

        GameObject newBallObject = Instantiate(ballPrefab, position, Quaternion.identity);
        Rigidbody2D newRigidBody = newBallObject.GetComponent<Rigidbody2D>();
        newRigidBody.position = position;
        newRigidBody.velocity = rightDirection * ball.CurrentSpeed;
        Ball newBall = newBallObject.GetComponent<Ball>();
        newBall.CurrentSpeed = ball.CurrentSpeed;

        GameController.Instance.powerUpSounds.PlayOneShot(splitSound);
    }

    public void ResetBalls()
    {
        balls.Clear();
    }
}
