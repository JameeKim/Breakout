using System.Collections;
using UnityEngine;

public class PowerUpGrow : PowerUp
{
    public float duration = 3.0f;

    private Paddle paddle;

    private void Start()
    {
        paddle = GameController.Instance.paddle;
    }

    protected override void ApplyPowerUp()
    {
        float originalWidth = paddle.width;
        paddle.width *= 1.25f;
        paddle.ApplySize();
        StartCoroutine(RevertPaddleWidth(duration, paddle, originalWidth));
    }

    private static IEnumerator RevertPaddleWidth(float duration, Paddle paddle, float originalWidth)
    {
        yield return new WaitForSeconds(duration);
        paddle.width = originalWidth;
        paddle.ApplySize();
    }
}
